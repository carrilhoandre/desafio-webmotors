
import { Injector } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable, throwError } from "rxjs";


export abstract class BaseService{

  protected http: HttpClient;

  constructor(
    protected apiPath: string, 
    protected injector: Injector
  ){
    this.http = injector.get(HttpClient);  
  }

  protected handleError(error: any): Observable<any>{
    console.log("ERRO NA REQUISIÇÃO => ", error);
    return throwError(error);
  }

}