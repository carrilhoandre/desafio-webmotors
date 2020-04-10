import { Injector, AfterContentChecked, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { BaseResourceModel } from '../../models/base.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseResourceService } from '../../services/base.resource.service';
import { switchMap } from 'rxjs/operators';
import Swal from 'sweetalert2';

export abstract class BaseFormComponent<T extends BaseResourceModel>
  implements OnInit, AfterContentChecked {
  currentAction: string;
  resourceForm: FormGroup;
  pageTitle: string;
  serverErrorMessages: string[] = null;
  submittingForm: boolean = false;
  validEmailType: boolean = false;
  firstLoad = true;
  isLoading = true;
  protected listUrl = '';
  protected route: ActivatedRoute;
  protected router: Router;
  protected formBuilder: FormBuilder;

  constructor(
    protected injector: Injector,
    public resource: T,
    protected resourceService: BaseResourceService<T>,
    protected jsonDataToResourceFn: (jsonData) => T
  ) {
    this.route = this.injector.get(ActivatedRoute);
    this.router = this.injector.get(Router);
    this.formBuilder = this.injector.get(FormBuilder);
  }

  ngOnInit() {
    this.setCurrentAction();
    this.buildResourceForm();
    this.loadResource();
  }

  ngAfterContentChecked() {
    this.setPageTitle();
  }

  protected submitForm() {
    this.submittingForm = true;

    if (this.currentAction == 'novo') this.createResource();
    else this.updateResource();
  }

  back() {
    this.router.navigate([this.listUrl]);
  }

  emailValidationType(e) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(String(e).toLowerCase())) {
      this.validEmailType = true;
    } else {
      this.validEmailType = false;
    }
  }

  // PRIVATE METHODS
  private isNew(element, index, array) {
    if (element.path) return element.path.includes('novo');
    else return element == 'novo';
  }

  protected setCurrentAction() {
    if (this.route.snapshot.url.some(this.isNew)) this.currentAction = 'novo';
    else this.currentAction = 'alterar';
  }

  protected loadResource() {
    if (this.currentAction == 'alterar') {
      this.resourceForm
        .get('id')
        .setValue(this.route.snapshot.paramMap.get('id'));
      this.route.paramMap
        .pipe(
          switchMap((params) => this.resourceService.getById(+params.get('id')))
        )
        .subscribe(
          (resource) => {
            this.resource = resource;
            this.resourceForm.patchValue(resource); // binds loaded resource data to resourceForm
            this.isLoading = false;
            this.firstLoad = false;
          },
          (error) => alert('Ocorreu um erro no servidor, tente mais tarde.')
        );
    }
  }

  protected showNotification(message: string) {
    Swal.fire(message, '', 'success');
  }

  protected showError(message: string) {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      html: message,
    });
  }

  protected setPageTitle() {
    if (this.currentAction == 'novo') this.pageTitle = this.creationPageTitle();
    else {
      this.pageTitle = this.editionPageTitle();
    }
  }

  protected creationPageTitle(): string {
    return 'Novo';
  }

  protected editionPageTitle(): string {
    return 'Edição';
  }

  protected bindResource() {
    this.resource = this.jsonDataToResourceFn(this.resourceForm.value);
  }

  protected createResource() {
    this.bindResource();
    this.resourceService.create(this.resource).subscribe(
      (resource) => {
        this.actionsForSuccess(resource);
        this.submittingForm = false;
      },
      (error) => {
        this.actionsForError(error);
        this.submittingForm = false;
      }
    );
  }

  protected updateResource() {
    this.bindResource();
    this.resourceService.update(this.resource).subscribe(
      (resource) => {
        this.actionsForSuccess(resource);
        this.submittingForm = false;
      },
      (error) => {
        this.actionsForError(error);
        this.submittingForm = false;
      }
    );
  }

  protected actionsForSuccess(resource: T) {
    Swal.fire('Dados salvos com sucesso!', '', 'success');
    if (this.listUrl != '') this.router.navigate([this.listUrl]);
  }

  protected actionsForError(error) {
    var strErrors = '';
    error.errors.forEach((element) => {
      if (strErrors != '') strErrors = strErrors + '<br/>' + element.message;
      else strErrors = element.message;
    });
    this.showError(strErrors);
    this.submittingForm = false;

    if (error.status === 422)
      this.serverErrorMessages = JSON.parse(error._body).errors;
    else {
      this.serverErrorMessages = [];
      error.errors.forEach((element) => {
        this.serverErrorMessages.push(element.message);
      });
    }
  }

  public isFieldValid(form: FormGroup, field: string) {
    return !form.get(field).valid && form.get(field).touched;
  }

  public displayFieldCss(form: FormGroup, field: string) {
    return {
      'has-error': this.isFieldValid(form, field),
      'has-feedback': this.isFieldValid(form, field),
    };
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  protected abstract buildResourceForm(): void;
}
