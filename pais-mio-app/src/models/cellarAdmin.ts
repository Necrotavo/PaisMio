import { Cellar } from './cellar';
export class CellarAdmin {
    doBodega: Cellar;
    correoAdministrador: String;

    constructor(doBodega: Cellar, correoAdministrador: string){
        this.doBodega = doBodega;
        this.correoAdministrador = correoAdministrador;
    }
}