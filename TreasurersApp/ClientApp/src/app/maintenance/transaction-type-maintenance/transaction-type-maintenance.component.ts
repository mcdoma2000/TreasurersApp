import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { TransactionType } from '../../models/TransactionType';
import { TransactionCategory } from '../../models/TransactionCategory';
import { TransactionTypeService } from './transaction-type.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { SecurityService } from 'src/app/security/security.service';
import { TransactionTypeRequest } from 'src/app/models/TransactionTypeRequest';
import { TransactionCategoryService } from '../transaction-category-maintenance/transaction-category.service';

@Component({
  selector: 'app-transaction-type-maintenance',
  templateUrl: './transaction-type-maintenance.component.html',
  styleUrls: ['./transaction-type-maintenance.component.css']
})
export class TransactionTypeMaintenanceComponent implements OnInit {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  types: TransactionType[] = [];
  rowData: TransactionType[] = [];
  categories: TransactionCategory[] = [];
  foundTransactionType: TransactionType = null;
  rowIsSelected = false;
  selectedTransactionType: TransactionType = this.transactionTypeService.newTransactionType();
  ttypeToEdit: TransactionType = this.transactionTypeService.newTransactionType();
  displayEdit = false;
  displayAdd = false;

  constructor(private transactionTypeService: TransactionTypeService,
    private transactionCategoryService: TransactionCategoryService,
    private securityService: SecurityService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.transactionCategoryService.getTransactionCategories(true).subscribe(resp => { this.categories = resp; });
    this.transactionTypeService.getTransactionTypes(true).subscribe(resp => { this.rowData = resp; });
  }

  addTransactionType() {
    this.displayAdd = true;
    this.ttypeToEdit = this.transactionTypeService.newTransactionType();
  }

  deleteTransactionType() {
    this.ttypeToEdit =
      new TransactionType(
        this.selectedTransactionType.id,
        this.selectedTransactionType.transactionCategoryId,
        this.selectedTransactionType.name,
        this.selectedTransactionType.description,
        this.selectedTransactionType.displayOrder,
        this.selectedTransactionType.active,
        this.selectedTransactionType.createdBy,
        this.selectedTransactionType.createdDate,
        this.selectedTransactionType.lastModifiedBy,
        this.selectedTransactionType.lastModifiedDate);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Transaction Type Maintenance: Delete',
      detail: 'Deleting Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.transactionTypeService.deleteTransactionType(this.ttypeToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Type Maintenance: Delete',
              detail: resp.statusMessages[0]
            });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Transaction Type Maintenance: Delete',
              detail: 'An error occurred while attempting to delete a transaction type.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({
                severity: 'error',
                summary: 'Transaction Type Maintenance: Delete',
                detail: msg
              });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Transaction Type Maintenance: Delete',
            detail: JSON.stringify(err)
          });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'Transaction Type Maintenance: Add',
      detail: 'Adding Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      const request = new TransactionTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.ttypeToEdit;
      this.transactionTypeService.addTransactionType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Type Maintenance: Add',
              detail: resp.statusMessages[0]
            });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Transaction Type Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Transaction Type Maintenance: Add',
            detail: 'An exception occurred while attempting to add a transaction type.'
          });
          console.log(err);
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmUpdate() {
    const confMsg = {
      severity: 'warn',
      summary: 'Transaction Type Maintenance: Update',
      detail: 'Updating Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      const request = new TransactionTypeRequest();
      request.userName = this.securityService.loggedInUserName();
      request.data = this.ttypeToEdit;
      this.transactionTypeService.updateTransactionType(request).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Type Maintenance: Update',
              detail: resp.statusMessages[0]
            });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Transaction Type Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Transaction Type Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: TransactionTypeMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.transactionTypeService.getTransactionTypes(true).subscribe(
      (resp) => {
        this.rowData = resp;
        setTimeout(this.refreshData, 0, this);
      },
      (err) => {
        console.log(err);
        this.rowData = [];
        setTimeout(this.refreshData, 0, this);
      }
    );
  }

  showConfirmation(message: string, header: string, acceptLabel: string, confirmationMessage: ConfirmationMessage, callback: () => void) {
    this.confirmationService.confirm({
      message: message,
      header: header,
      icon: 'pi pi-info-circle',
      acceptLabel: acceptLabel,
      rejectLabel: 'Cancel',
      accept: () => {
        this.messageService.add(confirmationMessage);
        callback();
      },
      reject: () => {
        console.log('Rejected Update');
      }
    });
  }

  editTransactionType() {
    this.displayEdit = true;
    this.ttypeToEdit =
      new TransactionType(
        this.selectedTransactionType.id,
        this.selectedTransactionType.transactionCategoryId,
        this.selectedTransactionType.name,
        this.selectedTransactionType.description,
        this.selectedTransactionType.displayOrder,
        this.selectedTransactionType.active,
        this.selectedTransactionType.createdBy,
        this.selectedTransactionType.createdDate,
        this.selectedTransactionType.lastModifiedBy,
        this.selectedTransactionType.lastModifiedDate);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Transaction Type Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Transaction Type Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.transactionTypeService.validateTransactionType(this.ttypeToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedTransactionType.id &&
      this.transactionTypeService.validateTransactionType(this.selectedTransactionType) &&
      (this.selectedTransactionType.name !== this.ttypeToEdit.name ||
       this.selectedTransactionType.description !== this.ttypeToEdit.description ||
       this.selectedTransactionType.active === null)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.ttypeToEdit = this.transactionTypeService.newTransactionType();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.ttypeToEdit = this.transactionTypeService.newTransactionType();
  }

  onRowClick(e) {
    this.selectedTransactionType =
      new TransactionType(
        e.data.id,
        e.data.transactionCategoryId,
        e.data.name,
        e.data.description,
        e.data.displayOrder,
        e.data.active,
        e.data.createdBy,
        e.data.createdDate,
        e.data.lastModifiedBy,
        e.data.lastModifiedDate);
    this.rowIsSelected = true;
  }

  validateTransactionType(ctype: TransactionType): boolean {
    if (!ctype.name || !ctype.description || ctype.active === null) {
      return false;
    }
    return true;
  }

  getTransactionCategoryById(id: number): Observable<TransactionType> {
    if (this.rowData.length > 0) {
      this.foundTransactionType = this.rowData.find(x => x.id === id);
    }
    if (this.foundTransactionType) {
      return of(this.foundTransactionType);
    } else {
      return this.transactionTypeService.getTransactionTypeById(id);
    }
  }

  getTransactionCategories(forceReload: boolean = false): Observable<TransactionType[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.transactionTypeService.getTransactionTypes().subscribe(
        (resp) => {
          this.rowData = resp;
          return of(this.rowData);
        },
        (err) => {
          console.log(err);
          this.rowData = [];
          return of(this.rowData);
        }
      );
    }
  }
}
