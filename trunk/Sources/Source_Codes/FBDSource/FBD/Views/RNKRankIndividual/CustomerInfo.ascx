﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FBD.ViewModels.RNKCustomerInfo>" %>


    <b>CIF Number:<span class="brownText" ><%=Model.CIF%></span></b><br/>
	<b>Customer Name:<span class="brownText" ><%=Model.CustomerName %></span></b> <br/>
	<b>Date: <span class="brownText" ><%=Model.Date%></span></b> <br/>
	<b>Branch:<span class="brownText" ><%=Model.Branch %></span></b> 
	<hr/>
