import { Injectable, Injector } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class MarcaService extends BaseService{
    protected http: HttpClient;

  constructor(protected injector: Injector) {
    super(environment.webmotorsApiUrl + "OnlineChallenge/Make", injector);
  }

  obterMarcas() : any
  {
      return this.http.get(this.apiPath);
  }
}