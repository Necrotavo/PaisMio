import { ProductInOrder } from './productInOrder';
import { Client } from './client';
import { Analysis } from './analysis';
import {AnalysisPC} from './analysisPC';
export class Order {
    codigo: number;
    cliente: Client;
    correoAdminIngreso: string;
    correoAdminDespacho: string;
    estado: string;
    fechaIngreso: string;
    fechaDespacho: string;
    listaProductos: ProductInOrder[];
    doAnalisisAA: Analysis;

    constructor(codigo: number, cliente: Client, correoAdminIngreso: string, 
                listaProductos: ProductInOrder[]){

        this.codigo = codigo;
        this.cliente = cliente;
        this.correoAdminIngreso = correoAdminIngreso;
        this.listaProductos = listaProductos;

        this.doAnalisisAA = new Analysis(0, 0, 0, 0, 0, 0, '', '', '', '', new Array <AnalysisPC>());

    }

}
