import { Order } from './order';
import { InfoPaisMio } from './infoPaisMio';
export class OrderReport {
    listaPedidos: Order[];
    infoPaisMio: InfoPaisMio;
    fechaInicio: string;
    fechaFinal: string;

    constructor(listaPedidos: Order[], infoPaisMio: InfoPaisMio, fechaInicio: string, fechaFinal: string){
        this.listaPedidos = listaPedidos;
        this.infoPaisMio = infoPaisMio;
        this.fechaInicio = fechaInicio;
        this.fechaFinal = fechaFinal;
    }
}