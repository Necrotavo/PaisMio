export class InfoPaisMio {
    codigo: number;
    nombre: string;
    cedulaJuridica: string;
    correo: string;
    telefono: string;
    direccion: string;
    logo: string;

    constructor(codigo: number, nombre: string, cedulaJuridica: string, correo: string, telefono: string, direccion: string, logo: string){
        this.codigo = codigo;
        this.nombre = nombre;
        this.cedulaJuridica = cedulaJuridica;
        this.correo = correo;
        this.telefono = telefono;
        this.direccion = direccion;
        this.logo = logo;
    }
}
