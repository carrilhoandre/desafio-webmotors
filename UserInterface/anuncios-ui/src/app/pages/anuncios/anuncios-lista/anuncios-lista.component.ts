import { Component, OnInit } from '@angular/core';
import { AnuncioService } from 'src/app/core/services/anuncios/anuncio.service';
import { Anuncio } from 'src/app/core/models/anuncios/anuncio.model';
import { BaseListComponent } from 'src/app/core/components/base-form/base-list.component';

@Component({
  selector: 'app-anuncios-lista',
  templateUrl: './anuncios-lista.component.html',
  styleUrls: ['./anuncios-lista.component.scss']
})
  export class AnunciosListaComponent extends BaseListComponent<Anuncio> {
    constructor(private _anuncioService: AnuncioService) {
      super(_anuncioService);
      this.searchCommand = {
        page: 1
      };
    }
  }