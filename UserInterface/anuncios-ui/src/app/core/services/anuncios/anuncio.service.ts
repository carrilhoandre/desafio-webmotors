import { Injectable, Injector } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BaseResourceService } from '../base.resource.service';
import { Anuncio } from '../../models/anuncios/anuncio.model';

@Injectable({
  providedIn: 'root'
})
export class AnuncioService extends BaseResourceService<Anuncio> {

  constructor(protected injector: Injector) {
    super(environment.apiBaseUrl + "anuncio", injector, Anuncio.fromJson);
  }
}