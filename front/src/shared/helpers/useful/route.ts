import { NavigationExtras, Router } from "@angular/router";

export const ex_callRouteWithObject = (routeUrl:string, entity: any, _router: Router) => {

    const objectRoute: NavigationExtras = {
      state:entity
    };

    _router.navigate([routeUrl], objectRoute);
  }