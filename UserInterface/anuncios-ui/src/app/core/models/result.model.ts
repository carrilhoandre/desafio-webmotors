import { BaseResourceModel } from './base.model';

export abstract class ResultModel<T extends BaseResourceModel>{
    totalPaginas?: number;
    total?: number;
    pagina?: number;
    itens?: T[];
  }
