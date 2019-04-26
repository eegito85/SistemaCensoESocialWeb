import { CanLoad, Route, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate} from "@angular/router";
import { Injectable } from "@angular/core";
import { LoginService } from "../login/login.service";

@Injectable()
export class AuthGuard implements CanLoad,CanActivate {

    constructor(private loginService: LoginService){

    }

    checkAuthentication(path: string){
        const usuarioLogado = this.loginService.isAuthenticated();
        if (usuarioLogado){
            return usuarioLogado;
        } else {
            this.loginService.redirectUnauthorized();
            return false;
        }
    }

    canLoad(route: Route): boolean{
    return this.checkAuthentication(route.path);

    }

    canActivate(activatedRoute: ActivatedRouteSnapshot,routerState: RouterStateSnapshot): boolean{
        return this.checkAuthentication(activatedRoute.routeConfig.path);
    }
}