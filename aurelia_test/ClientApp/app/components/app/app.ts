import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';

export class App {
    router: Router | undefined;

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'aurelia_test';
        config.map([{
            route: ['', 'home'],
            name: 'home',
            settings: { icon: 'home' },
            moduleId: PLATFORM.moduleName('../home/home'),
            nav: true,
            title: 'Home'
        }, {
            route: ['', 'StartGame'],
            name: 'StartGame',
            settings: { icon: 'StartGame' },
            moduleId: PLATFORM.moduleName('../StartGame/StartGame'),
            nav: true,
            title: 'Start Game'
        }]);

        this.router = router;
    }
}
