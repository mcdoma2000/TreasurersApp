import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { ContributionCategory } from '../../models/ContributionCategory';
import { ContributionCategoryService } from './contribution-category.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-contribution-category-maintenance',
  templateUrl: './contribution-category-maintenance.component.html',
  styleUrls: ['./contribution-category-maintenance.component.css']
})
export class ContributionCategoryMaintenanceComponent implements OnInit {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  categories: ContributionCategory[] = [];
  rowData: ContributionCategory[] = [];
  foundContributionCategory: ContributionCategory = null;
  rowIsSelected = false;
  selectedContributionCategory = new ContributionCategory(0, null, null);
  ccatToEdit = new ContributionCategory(0, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private contributionCategoryService: ContributionCategoryService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.contributionCategoryService.getContributionCategories(true).subscribe(resp => { this.rowData = resp; });
  }

  ngOnDestroy() {
  }

  addContributionCategory() {
    this.displayAdd = true;
    this.ccatToEdit = this.contributionCategoryService.newContributionCategory();
  }

  deleteContributionCategory() {
    this.ccatToEdit =
      new ContributionCategory(this.selectedContributionCategory.id,
        this.selectedContributionCategory.contributionCategoryName,
        this.selectedContributionCategory.description);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Contribution Type Maintenance: Delete',
      detail: 'Deleting Contribution Type...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.contributionCategoryService.deleteContributionCategory(this.ccatToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Contribution Type Maintenance: Delete', detail: resp.statusMessages[0] });
          } else {
            this.messageService.add({ severity: 'error', summary: 'ContributionCategory Maintenance: Delete', detail: 'An error occurred while attempting to delete an address.' });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'ContributionCategory Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'ContributionCategory Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'ContributionCategory Maintenance: Add',
      detail: 'Adding Contribution Type...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      this.contributionCategoryService.addContributionCategory(this.ccatToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Contribution Type Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Contribution Type Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Contribution Type Maintenance: Add', detail: 'An exception occurred while attempting to add a contribution type.' });
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
      summary: 'Contribution Type Maintenance: Update',
      detail: 'Updating Contribution Type...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      this.contributionCategoryService.updateContributionCategory(this.ccatToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Contribution Type Maintenance: Update', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Contribution Type Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Contribution Type Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: ContributionCategoryMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.contributionCategoryService.getContributionCategories(true).subscribe(
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

  editContributionCategory() {
    this.displayEdit = true;
    this.ccatToEdit =
      new ContributionCategory(this.selectedContributionCategory.id,
        this.selectedContributionCategory.contributionCategoryName,
        this.selectedContributionCategory.description);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'ContributionCategory Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'ContributionCategory Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.contributionCategoryService.validateContributionCategory(this.ccatToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedContributionCategory.id &&
        this.contributionCategoryService
          .validateContributionCategory(this.selectedContributionCategory) &&
       (this.selectedContributionCategory.contributionCategoryName !== this.ccatToEdit.contributionCategoryName ||
        this.selectedContributionCategory.description !== this.ccatToEdit.description)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.ccatToEdit = this.contributionCategoryService.newContributionCategory();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.ccatToEdit = this.contributionCategoryService.newContributionCategory();
  }

  onRowClick(e) {
    this.selectedContributionCategory = new ContributionCategory(e.data.id, e.data.contributionCategoryName, e.data.description);
    this.rowIsSelected = true;
  }

  validateContributionCategory(ctype: ContributionCategory): boolean {
    if (!ctype.contributionCategoryName || !ctype.description) {
      return false;
    }
    return true;
  }

  getContributionCategoryById(id: number): Observable<ContributionCategory> {
    if (this.rowData.length > 0) {
      this.foundContributionCategory = this.rowData.find(x => x.id === id);
    }
    if (this.foundContributionCategory) {
      return of(this.foundContributionCategory);
    } else {
      return this.contributionCategoryService.getContributionCategoryById(id);
    }
  }

  getContributionCategories(forceReload: boolean = false): Observable<ContributionCategory[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.contributionCategoryService.getContributionCategories().subscribe(
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
