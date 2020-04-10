import { BaseResourceModel } from '../base.model';

export class Anuncio extends BaseResourceModel {
  constructor(
    public id?:number,
    public marca?: string,
    public modelo?: string,
    public versao?: string,
    public ano?: number,
    public quilometragem?: number,
    public observacao?: string,
  ){
    super();
  }
  

  static fromJson(jsonData: any): Anuncio {
    return Object.assign(new Anuncio(), jsonData);
  }
}