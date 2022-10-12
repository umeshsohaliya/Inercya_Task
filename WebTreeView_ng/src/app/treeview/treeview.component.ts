import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 


@Component({
  selector: 'app-treeview',
  templateUrl: './treeview.component.html',
  styleUrls: ['./treeview.component.css']
})
export class TreeviewComponent implements OnInit {

  constructor(private http: HttpClient) { }
  
  items:any;

  ngOnInit(): void {
this.http.get("./assets/Items.json").subscribe(data => {
  this.items=data;
});

  }
}
