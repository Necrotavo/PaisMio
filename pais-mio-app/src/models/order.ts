import { ProductInOrder } from './productInOrder';
export class Order {
    codigo: number;
    cedulaCliente: string;
    correoAdminIngreso: string;
    correoAdminDespacho: string;
    estado: string;
    fecheIngreso: string;
    fechaDespacho: string;
    listaProductos: ProductInOrder[];

    constructor(codigo: number, cedulaCliente: string, correoAdminIngreso: string, listaProductos: ProductInOrder[]){

        this.codigo = codigo;
        this.cedulaCliente = cedulaCliente;
        this.correoAdminIngreso = correoAdminIngreso;
        this.listaProductos = listaProductos;

    }

}