import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { TransactionCategory } from '../../models/TransactionCategory';
import { TransactionCategoryService } from './transaction-category.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-transaction-category-maintenance',
  templateUrl: './transaction-category-maintenance.component.html',
  styleUrls: ['./transaction-category-maintenance.component.css']
})
export class TransactionCategoryMaintenanceComponent implements OnInit {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  categories: TransactionCategory[] = [];
  rowData: TransactionCategory[] = [];
  foundTransactionCategory: TransactionCategory = null;
  rowIsSelected = false;
  selectedTransactionCategory = this.transactionCategoryService.newTransactionCategory();
  tcatToEdit = this.transactionCategoryService.newTransactionCategory();
  displayEdit = false;
  displayAdd = false;

  constructor(private transactionCategoryService: TransactionCategoryService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.transactionCategoryService.getTransactionCategories(true).subscribe(resp => { this.rowData = resp; });
  }

  addTransactionCategory() {
    this.displayAdd = true;
    this.tcatToEdit = this.transactionCategoryService.newTransactionCategory();
  }

  deleteTransactionCategory() {
    this.tcatToEdit =
      new TransactionCategory(this.selectedTransactionCategory.id,
        this.selectedTransactionCategory.name,
        this.selectedTransactionCategory.description,
        this.selectedTransactionCategory.displayOrder,
        this.selectedTransactionCategory.active);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Transaction Category Maintenance: Delete',
      detail: 'Deleting Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.transactionCategoryService.deleteTransactionCategory(this.tcatToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Category Maintenance: Delete',
              detail: resp.statusMessages[0]
            });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Transaction Category Maintenance: Delete',
              detail: 'An error occurred while attempting to delete a transaction category.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({
                severity: 'error',
                summary: 'Transaction Category Maintenance: Delete',
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
            summary: 'Transaction Category Maintenance: Delete',
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
      summary: 'Transaction Category Maintenance: Add',
      detail: 'Adding Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      this.transactionCategoryService.addTransactionCategory(this.tcatToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Category Maintenance: Add',
              detail: resp.statusMessages[0]
            });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Transaction Category Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Transaction Category Maintenance: Add',
            detail: 'An exception occurred while attempting to add a Transaction Category.'
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
      summary: 'Transaction Category Maintenance: Update',
      detail: 'Updating Transaction Category...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      this.transactionCategoryService.updateTransactionCategory(this.tcatToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Transaction Category Maintenance: Update',
              detail: resp.statusMessages[0]
            });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Transaction Category Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Transaction Category Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: TransactionCategoryMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.transactionCategoryService.getTransactionCategories(true).subscribe(
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

  editTransactionCategory() {
    this.displayEdit = true;
    this.tcatToEdit =
      new TransactionCategory(this.selectedTransactionCategory.id,
        this.selectedTransactionCategory.name,
        this.selectedTransactionCategory.description,
        this.selectedTransactionCategory.displayOrder,
        this.selectedTransactionCategory.active);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Transaction Category Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Transaction Category Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.transactionCategoryService.validateTransactionCategory(this.tcatToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedTransactionCategory.id &&
      this.transactionCategoryService
        .validateTransactionCategory(this.selectedTransactionCategory) &&
      (this.selectedTransactionCategory.name !== this.tcatToEdit.name ||
        this.selectedTransactionCategory.description !== this.tcatToEdit.description)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.tcatToEdit = this.transactionCategoryService.newTransactionCategory();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.tcatToEdit = this.transactionCategoryService.newTransactionCategory();
  }

  onRowClick(e) {
    this.selectedTransactionCategory =
      new TransactionCategory(e.data.id, e.data.transactionCategoryName, e.data.description, e.data.displayOrder, e.data.active);
    this.rowIsSelected = true;
  }

  validateTransactionCategory(ctype: TransactionCategory): boolean {
    if (!ctype.name || !ctype.description) {
      return false;
    }
    return true;
  }

  getTransactionCategoryById(id: number): Observable<TransactionCategory> {
    if (this.rowData.length > 0) {
      this.foundTransactionCategory = this.rowData.find(x => x.id === id);
    }
    if (this.foundTransactionCategory) {
      return of(this.foundTransactionCategory);
    } else {
      return this.transactionCategoryService.getTransactionCategoryById(id);
    }
  }

  getTransactionCategories(forceReload: boolean = false): Observable<TransactionCategory[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.transactionCategoryService.getTransactionCategories().subscribe(
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
