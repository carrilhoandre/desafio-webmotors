import { Injectable, Injector } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class VersaoService extends BaseService{
    protected http: HttpClient;

  constructor(protected injector: Injector) {
    super(environment.webmotorsApiUrl + "OnlineChallenge/Version", injector);
  }

  obterVersoesPorModelo(modeloId) : any
  {
      return this.http.get(`${this.apiPath}?ModelID=${modeloId}`);
  }
}