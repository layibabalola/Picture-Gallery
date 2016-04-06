<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Picture_Gallery.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Catch us at the next family reunion!</h3>
    <address>
        One Babalola Way<br />
        Houston, TX 77002<br />
        <abbr title="Phone">P:</abbr>
        281.555.1000
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@babalola.com">Support@babalola.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@babalola.com">Marketing@babalola.com</a>
    </address>
</asp:Content>
