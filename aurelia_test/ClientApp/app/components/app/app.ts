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
            route: ['Game'],
            name: 'Game',
            settings: { icon: 'Game' },
            moduleId: PLATFORM.moduleName('../Game/Game'),
            nav: true,
            title: 'Game'
        }, {
            route: ['Slagen'],
            name: 'Slagen',
            settings: { icon: 'Slagen' },
            moduleId: PLATFORM.moduleName('../Slagen/Slagen'),
            nav: true,
            title: 'Slagen per speler'
        }
        ]);

        this.router = router;
    }
}
