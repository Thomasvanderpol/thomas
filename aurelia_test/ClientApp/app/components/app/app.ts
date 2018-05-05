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
            route: 'forum',
            name: 'forum',
            settings: { icon: 'education' },
            moduleId: PLATFORM.moduleName('../forum/forum'),
            nav: true,
            title: 'Forum'
        }, {
            route: 'fetch-data',
            name: 'fetchdata',
            settings: { icon: 'th-list' },
            moduleId: PLATFORM.moduleName('../fetchdata/fetchdata'),
            nav: true,
            title: 'Fetch data'
        }, {
            route: 'newuser',
            name: 'newuser',
            settings: { icon: 'education' },
            moduleId: PLATFORM.moduleName('../newuser/newuser'),
            nav: true,
            title: 'New User'
        }, {
            route: 'preparationround',
            name: 'preparationround',
            settings: { icon: 'th-list' },
            moduleId: PLATFORM.moduleName('../preparationround/preparationround'),
            nav: false,
            title: 'Preparation Round'
        }]);

        this.router = router;
    }
}
