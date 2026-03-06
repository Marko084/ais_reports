<%@ page language="C#" autoeventwireup="true" inherits="reports_PrintCheck, App_Web_printcheck.aspx.dfa151d5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
          background: rgb(204,204,204); 
        }
        page[size="A4"] {
          background: white;
          width: 21cm;
          height: 29.7cm;
          display: block;
          margin: 0 auto;
          margin-bottom: 0.5cm;
          box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
          vertical-align:text-bottom;
        }
        @media print {
          body, page[size="A4"] {
            margin: 0;
            box-shadow: 0;
          }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <page size="A4">
        <span>Hello 1!</span>
    </page>
    <page size="A4">
        <span>Hello 2!</span>
    </page>
    <page size="A4">
        <span>Hello 3!</span>
    </page>
    </form>
</body>
</html>
