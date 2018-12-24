import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { ContributionType } from '../../models/ContributionType';
import { ContributionTypeService } from './contribution-type.service';
import { ContributionTypeViewModel } from '../../models/ContributionTypeViewModel';
import { ContributionCategory } from '../../models/ContributionCategory';
import { ContributionCategoryService } from '../contribution-category-maintenance/contribution-category.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-address-maintenance',
  templateUrl: './contribution-type-maintenance.component.html',
  styleUrls: ['./contribution-type-maintenance.component.css']
})
export class ContributionTypeMaintenanceComponent implements OnInit, OnDestroy {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: ContributionTypeViewModel[] = [];
  categories: SelectItem[] = [];
  foundContributionType: ContributionType = null;
  rowIsSelected = false;
  selectedContributionType = new ContributionType(0, null, null, null, null, null);
  ctypeToEdit = new ContributionType(0, null, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private contributionTypeService: ContributionTypeService,
    private contributionCategoryService: ContributionCategoryService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.contributionTypeService.getContributionTypeViewModels(true).subscribe(
      (resp) => {
        this.rowData = resp;
        console.log('Contribution Types: ' + resp.length);
      },
      (err) => {
        console.log(JSON.stringify(err));
      }
    );
    let cats: ContributionCategory[] = [];
    this.contributionCategoryService.getContributionCategories(true).subscribe(
      (resp) => {
        cats = resp;
        console.log('Contribution Categories: ' + resp.length);
        this.categories = cats.map(function (value) {
          return { label: value.description, value: value.id };
        });
        console.log('Dropdown Count: ' + this.categories.length);
          },
      (err) => {
        console.log(JSON.stringify(err));
        this.categories = [];
      }
    );
  }

  ngOnDestroy() {
  }

  addContributionType() {
    this.displayAdd = true;
    this.ctypeToEdit = this.contributionTypeService.newContributionType();
  }

  deleteContributionType() {
    this.ctypeToEdit =
      new ContributionType(this.selectedContributionType.id,
        this.selectedContributionType.contributionCategoryId,
        this.selectedContributionType.contributionTypeName,
        this.selectedContributionType.description,
        this.selectedContributionType.displayOrder,
        this.selectedContributionType.active);
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
      this.contributionTypeService.deleteContributionType(this.ctypeToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Contribution Type Maintenance: Delete',
              detail: resp.statusMessages[0]
            });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'ContributionType Maintenance: Delete',
              detail: 'An error occurred while attempting to delete an address.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'ContributionType Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'ContributionType Maintenance: Delete', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  confirmAdd() {
    const confMsg = {
      severity: 'warn',
      summary: 'ContributionType Maintenance: Add',
      detail: 'Adding Contribution Type...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      this.contributionTypeService.addContributionType(this.ctypeToEdit).subscribe(
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
          this.messageService.add({
            severity: 'error',
            summary: 'Contribution Type Maintenance: Add',
            detail: 'An exception occurred while attempting to add a contribution type.'
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
      summary: 'Contribution Type Maintenance: Update',
      detail: 'Updating Contribution Type...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      this.contributionTypeService.updateContributionType(this.ctypeToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Contribution Type Maintenance: Update',
              detail: resp.statusMessages[0]
            });
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

  private refreshData(component: ContributionTypeMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.contributionTypeService.getContributionTypeViewModels(true).subscribe(
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

  editContributionType() {
    this.displayEdit = true;
    this.ctypeToEdit =
      new ContributionType(this.selectedContributionType.id,
        this.selectedContributionType.contributionCategoryId,
        this.selectedContributionType.contributionTypeName,
        this.selectedContributionType.description,
        this.selectedContributionType.displayOrder,
        this.selectedContributionType.active
        );
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'ContributionType Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'ContributionType Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.contributionTypeService.validateContributionType(this.ctypeToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedContributionType.id &&
      (this.selectedContributionType.contributionCategoryId !== this.ctypeToEdit.contributionCategoryId ||
      this.selectedContributionType.contributionTypeName !== this.ctypeToEdit.contributionTypeName ||
      this.selectedContributionType.description !== this.ctypeToEdit.description ||
      this.selectedContributionType.displayOrder !== this.ctypeToEdit.displayOrder ||
      this.selectedContributionType.active !== this.ctypeToEdit.active)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.ctypeToEdit = this.contributionTypeService.newContributionType();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.ctypeToEdit = this.contributionTypeService.newContributionType();
  }

  onRowClick(e) {
    this.selectedContributionType =
      new ContributionType(e.data.id,
        e.data.contributionTypeCategory,
        e.data.contributionTypeName,
        e.data.description,
        e.data.displayOrder,
        e.data.active);
    this.rowIsSelected = true;
  }

  validateContributionType(ctype: ContributionType): boolean {
    if (!ctype.contributionCategoryId || !ctype.contributionTypeName || !ctype.description || !ctype.displayOrder) {
      return false;
    }
    return true;
  }

  getContributionTypeById(id: number): Observable<ContributionType> {
    if (this.rowData.length > 0) {
      this.foundContributionType = this.rowData.find(x => x.id === id);
    }
    if (this.foundContributionType) {
      return of(this.foundContributionType);
    } else {
      return this.contributionTypeService.getContributionTypeById(id);
    }
  }

  getContributionTypes(forceReload: boolean = false): Observable<ContributionType[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.contributionTypeService.getContributionTypeViewModels().subscribe(
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
