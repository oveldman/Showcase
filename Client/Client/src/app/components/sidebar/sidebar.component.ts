import { Component, OnInit } from "@angular/core";

declare interface RouteInfo {
  path: string;
  title: string;
  rtlTitle: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  {
    path: "/dashboard",
    title: "Home",
    rtlTitle: "لوحة القيادة",
    icon: "icon-planet",
    class: ""
  },
  {
    path: "/oscar",
    title: "Curriculum vitae",
    rtlTitle: "الرموز",
    icon: "icon-badge",
    class: ""
  },
  {
    path: "/user",
    title: "Bugs Business",
    rtlTitle: "ملف تعريفي للمستخدم",
    icon: "icon-bulb-63",
    class: ""
  },
  {
    path: "/maps",
    title: "Projects",
    rtlTitle: "خرائط",
    icon: "icon-components",
    class: "" },
  {
    path: "/notifications",
    title: "Blog",
    rtlTitle: "إخطارات",
    icon: "icon-pencil",
    class: ""
  },
  {
    path: "/typography",
    title: "Tryouts",
    rtlTitle: "طباعة",
    icon: "icon-settings-gear-63",
    class: ""
  },
  {
    path: "/tables",
    title: "Links",
    rtlTitle: "قائمة الجدول",
    icon: "icon-light-3",
    class: ""
  },
  {
    path: "/rtl",
    title: "Contact",
    rtlTitle: "ار تي ال",
    icon: "icon-satisfied",
    class: ""
  }
];

@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.css"]
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() {}

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
    if (window.innerWidth > 991) {
      return false;
    }
    return true;
  }
}
