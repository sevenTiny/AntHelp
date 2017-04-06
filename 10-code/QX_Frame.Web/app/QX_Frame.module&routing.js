"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const platform_browser_1 = require("@angular/platform-browser");
const router_1 = require("@angular/router");
const platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
/* common app component-> 00-* */
const app_component_1 = require("./00-main/app.component"); //the root component
const index_component_1 = require("./01-index/index.component"); //the index component
/* start define components --there we add in ->-------------- 01 */
const example_component_1 = require("./02-example/example.component");
const signup_component_1 = require("./03-login/signup.component");
/* end define components */
const appRoutes = [
    {
        path: '',
        component: index_component_1.IndexComponent
    },
    {
        path: 'index',
        component: index_component_1.IndexComponent
    },
    /* start define components --there we add in ->----------- 02 */
    {
        path: 'example',
        component: example_component_1.exampleComponent
    },
    {
        path: 'signup',
        component: signup_component_1.SignUpComponent
    },
    /* end define components */
    {
        path: '**',
        component: index_component_1.IndexComponent
    },
];
const appComponents = [
    /* common app component-> 00-* */
    app_component_1.AppComponent,
    index_component_1.IndexComponent,
    /* start define components -- there we add in ->------------ 03 */
    example_component_1.exampleComponent,
    signup_component_1.SignUpComponent
    /* end define components */
];
/**
 * !!! do not edit the flowing must existing items --qixiao
 */
exports.routing = router_1.RouterModule.forRoot(appRoutes);
let QX_Frame_AppModule = class QX_Frame_AppModule {
};
QX_Frame_AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, exports.routing],
        declarations: appComponents,
        bootstrap: [app_component_1.AppComponent]
    })
], QX_Frame_AppModule);
exports.QX_Frame_AppModule = QX_Frame_AppModule;
const platform = platform_browser_dynamic_1.platformBrowserDynamic();
platform.bootstrapModule(QX_Frame_AppModule);
//# sourceMappingURL=QX_Frame.module&routing.js.map