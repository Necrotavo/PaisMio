import { Order } from './order';
import { InfoPaisMio } from './infoPaisMio';
export class OrderReport {
    listaPedidos: Order[];
    infoPaisMio: InfoPaisMio;
    mes: number;
    anho: number;

    constructor(listaPedidos: Order[], infoPaisMio: InfoPaisMio, mes: number, anho: number){
        this.listaPedidos = listaPedidos;
        this.infoPaisMio = infoPaisMio;
        this.mes = mes;
        this.anho = anho;
    }
}