"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
const core_1 = require('@angular/core');
const usefulllink_service_1 = require('../usefullLinksService/usefulllink.service');
let PersonalCabinet = class PersonalCabinet {
    constructor(linkService) {
        this.linkService = linkService;
        this._usefullLinkService = linkService;
    }
    get LinkList() { return this._linkList; }
    ngOnInit() {
        this._linkList = this._usefullLinkService.GetLinks();
    }
};
PersonalCabinet = __decorate([
    core_1.Component({
        selector: 'personal-cabinet',
        templateUrl: '/app/personalCabinet/personalCabinet.component.template.html',
        providers: [usefulllink_service_1.UsefullLinkService]
    }), 
    __metadata('design:paramtypes', [usefulllink_service_1.UsefullLinkService])
], PersonalCabinet);
exports.PersonalCabinet = PersonalCabinet;
//# sourceMappingURL=personalCabinet.component.js.map