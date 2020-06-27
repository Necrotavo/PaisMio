import { InputQ } from './inputQ';
import { Cellar } from './cellar';
export class EntryInput {
    doBodega: Cellar;
    insumo: InputQ;

    constructor(doBodega: Cellar, insumo: InputQ){
        this.doBodega = doBodega;
        this.insumo = insumo;
    }
}
