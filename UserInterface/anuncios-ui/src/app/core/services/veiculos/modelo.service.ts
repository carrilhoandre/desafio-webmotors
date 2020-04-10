import { Injectable, Injector } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class ModeloService extends BaseService{
    protected http: HttpClient;

  constructor(protected injector: Injector) {
    super(environment.webmotorsApiUrl + "OnlineChallenge/Model", injector);
  }

  obterModelosPorMarca(marcaID) : any
  {
      return this.http.get(`${this.apiPath}?MakeID=${marcaID}`);
  }
}