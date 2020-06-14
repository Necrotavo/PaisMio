import { Cellar } from './cellar';
export class CellarAdmin {
    doBodega: Cellar;
    correoAdministrador: string;

    constructor(doBodega: Cellar, correoAdministrador: string){
        this.doBodega = doBodega;
        this.correoAdministrador = correoAdministrador;
    }
}
