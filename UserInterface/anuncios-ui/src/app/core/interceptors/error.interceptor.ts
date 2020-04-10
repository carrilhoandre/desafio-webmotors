import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
        location.reload(true);
      }
      const error = err.error;
      var strErrors : string = '';
      error.errors.forEach(element => {
        strErrors = strErrors + element.message + " ";
      });
  
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: strErrors
      });
      return throwError(error);
    }))
  }
}
