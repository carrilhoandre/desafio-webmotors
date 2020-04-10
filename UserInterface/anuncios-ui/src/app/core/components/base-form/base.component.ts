import { OnInit, Injector } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

export abstract class BaseComponent implements OnInit {
  submittingForm: boolean = false;
  serverErrorMessages: string[] = null;
  route: ActivatedRoute;
  router: Router;
  constructor(protected injector: Injector) {
    this.route = injector.get(ActivatedRoute);
    this.router = injector.get(Router);
  }

  ngOnInit() {}

  protected showNotification(from, align, message: string) {
    Swal.fire(message, '', 'success');
  }

  protected actionsForError(error) {
    var strErrors = '';
    error.errors.forEach((element) => {
      strErrors = strErrors + element.message + ' ';
      if (strErrors == '') strErrors == element.message;
      else strErrors = strErrors + element.message + ' ';
    });

    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: strErrors,
    });

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
}
