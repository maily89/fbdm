<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Cluster</h2>
        <button id ="start" onclick="Initialize()" value="1" >Start</button>
        <button id ="next" onclick="Next()" >Next</button>
        <button id ="Run" onclick="Run()" >Run to end</button>
  
    <div id="partial">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Initialize() {
//            var ID = $("#PeriodReport").val();
//            if (ID > 0) {
            //                $("#partial").html("<img src='/Content/images/loading.gif' />");
//            var value = $("#start").val();
//            alert(value);
            $("#start").attr("disabled", true);
            $("#partial").load('/INVMiningByStep/Initialize');
           
        }

        function Next() {
            
            $("#partial").load('/INVMiningByStep/NextStep');
         
        }
        function Run() {

            $("#partial").load('/INVMiningByStep/Run');
         
        }
    </script>
</asp:Content>
