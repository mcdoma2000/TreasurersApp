import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { ConfirmationService, MessageService } from 'primeng/api';

import { Contributor } from '../../models/Contributor';
import { ContributorService } from './contributor.service';
import { ConfirmationMessage } from '../../models/ConfirmationMessage';
import { DxDataGridComponent } from 'devextreme-angular/ui/data-grid';

@Component({
  selector: 'app-contributor-maintenance',
  templateUrl: './contributor-maintenance.component.html',
  styleUrls: ['./contributor-maintenance.component.css']
})
export class ContributorMaintenanceComponent implements OnInit {

  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  uiBlocked = false;
  rowData: Contributor[] = [];
  foundContributor: Contributor = null;
  rowIsSelected = false;
  selectedContributor = new Contributor(0, null, null, null, null);
  contributorToEdit = new Contributor(0, null, null, null, null);
  displayEdit = false;
  displayAdd = false;

  constructor(private contributorService: ContributorService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService) {
  }

  ngOnInit() {
    this.contributorService.getContributors().subscribe(resp => { this.rowData = resp; });
  }

  addContributor() {
    this.displayAdd = true;
    this.contributorToEdit = this.contributorService.newContributor();
  }

  deleteContributor() {
    this.contributorToEdit =
      new Contributor(this.selectedContributor.id,
        this.selectedContributor.firstName,
        this.selectedContributor.middleName,
        this.selectedContributor.lastName,
        this.selectedContributor.addressId);
    this.confirmDelete();
  }

  confirmDelete() {
    const confMsg = {
      severity: 'warn',
      summary: 'Contributor Maintenance: Delete',
      detail: 'Deleting Contributor...'
    };
    this.showConfirmation('Are you certain that you want to delete this record?', 'Delete Confirmation', 'Delete', confMsg, () => {
      this.uiBlocked = true;
      this.contributorService.deleteContributor(this.contributorToEdit.id).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Contributor Maintenance: Delete',
              detail: resp.statusMessages[0]
            });
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Contributor Maintenance: Delete',
              detail: 'An error occurred while attempting to delete a contriburo.'
            });
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Contributor Maintenance: Delete', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          console.log(JSON.stringify(err));
          this.messageService.add({
            severity: 'error',
            summary: 'Contributor Maintenance: Delete',
            detail: 'An exception occurred while attempting to delete a contributor.'
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
      summary: 'Contributor Maintenance: Add',
      detail: 'Adding Contributor...'
    };
    this.showConfirmation('Are you certain that you want to add this record?', 'Add Confirmation', 'Add', confMsg, () => {
      this.uiBlocked = true;
      this.contributorService.addContributor(this.contributorToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({ severity: 'success', summary: 'Contributor Maintenance: Add', detail: resp.statusMessages[0] });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Contributor Maintenance: Add', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
          this.displayAdd = false;
        },
        (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Contributor Maintenance: Add',
            detail: 'An exception occurred while attempting to add a Contributor.'
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
      summary: 'Contributor Maintenance: Update',
      detail: 'Updating Contributor...'
    };
    this.showConfirmation('Are you certain that you want to update this record?', 'Update Confirmation', 'Update', confMsg, () => {
      this.uiBlocked = true;
      this.contributorService.updateContributor(this.contributorToEdit).subscribe(
        (resp) => {
          if (resp.success === true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Contributor Maintenance: Update',
              detail: resp.statusMessages[0]
            });
          } else {
            resp.statusMessages.forEach(function (msg) {
              this.messageService.add({ severity: 'error', summary: 'Contributor Maintenance: Update', detail: msg });
            });
          }
          this.refreshGridData();
          this.uiBlocked = false;
        },
        (err) => {
          this.messageService.add({ severity: 'error', summary: 'Contributor Maintenance: Update', detail: JSON.stringify(err) });
          this.refreshGridData();
          this.uiBlocked = false;
        }
      );
    });
  }

  private refreshData(component: ContributorMaintenanceComponent) {
    component.dataGrid.instance.getDataSource().reload();
    component.dataGrid.instance.refresh();
  }

  refreshGridData() {
    this.contributorService.getContributors().subscribe(
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

  editContributor() {
    this.displayEdit = true;
    this.contributorToEdit =
      new Contributor(this.selectedContributor.id,
        this.selectedContributor.firstName,
        this.selectedContributor.middleName,
        this.selectedContributor.lastName,
        this.selectedContributor.addressId);
  }

  saveAddClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveAdd()) {
      this.confirmAdd();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Contributor Maintenance: Add', detail: 'No data changed.' });
    }
  }

  saveEditClick($event) {
    this.displayEdit = false;
    if (this.shouldSaveEdit()) {
      this.confirmUpdate();
    } else {
      this.messageService.add({ severity: 'info', summary: 'Contributor Maintenance: Edit', detail: 'No data changed.' });
    }
  }

  private shouldSaveAdd(): boolean {
    const save = this.contributorService.validateContributor(this.contributorToEdit);
    return save;
  }

  private shouldSaveEdit(): boolean {
    let save = false;
    if (this.selectedContributor.id &&
        this.contributorService.validateContributor(this.selectedContributor) &&
       (this.selectedContributor.firstName !== this.contributorToEdit.firstName ||
        this.selectedContributor.lastName !== this.contributorToEdit.lastName ||
        this.selectedContributor.middleName !== this.contributorToEdit.middleName ||
        this.selectedContributor.addressId !== this.contributorToEdit.addressId)) {
      save = true;
    }
    return save;
  }

  cancelEditClick($event) {
    console.log('Cancel Edit was clicked');
    this.displayEdit = false;
    this.contributorToEdit = this.contributorService.newContributor();
  }

  cancelAddClick($event) {
    console.log('Cancel Add was clicked');
    this.displayAdd = false;
    this.contributorToEdit = this.contributorService.newContributor();
  }

  onRowClick(e) {
    this.selectedContributor =
      new Contributor(e.data.id, e.data.ContributorName, e.data.description, e.data.displayOrder, e.data.active);
    this.rowIsSelected = true;
  }

  validateContributor(contrib: Contributor): boolean {
    return this.contributorService.validateContributor(contrib);
  }

  getContributorById(id: number): Observable<Contributor> {
    if (this.rowData.length > 0) {
      this.foundContributor = this.rowData.find(x => x.id === id);
    }
    if (this.foundContributor) {
      return of(this.foundContributor);
    } else {
      return this.contributorService.getContributorById(id);
    }
  }

  getContributionCategories(forceReload: boolean = false): Observable<Contributor[]> {
    if (this.rowData.length > 0 && forceReload === false) {
      return of(this.rowData);
    } else {
      this.contributorService.getContributors().subscribe(
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
