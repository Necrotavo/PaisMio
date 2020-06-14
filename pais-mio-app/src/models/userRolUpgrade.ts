import { User } from './user';
export class UserRolUpgrade {
    usuario: User;
    rolNuevo: string;

    constructor(usuario: User, rolNuevo: string) {
            this.usuario = usuario;
            this.rolNuevo = rolNuevo;
    }
}
