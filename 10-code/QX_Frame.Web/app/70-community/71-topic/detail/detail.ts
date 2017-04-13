﻿import { Component, OnInit } from '@angular/core';

declare function escape(s: string): string;
//注入器的两种：NgModule/Component(只在当前及子组件中生效)
@Component({
    selector: 'detail',
    templateUrl: 'app/70-community/71-topic/detail/detail.html',
    styleUrls: ['app/70-community/71-topic/detail/detail.css'],
    providers: []   //元数据中申明依赖
})

export class Detail implements OnInit {
    
    ////the final execute ...
    ngOnInit(): void {
       
    }
}