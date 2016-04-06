<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Picture_Gallery._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />

    <p class="centeredItem">
        <asp:DataList ID="lstGalleries" runat="server" EnableViewState="false" CssClass="centeredItem" OnItemDataBound="lstGalleries_ItemDataBound" OnItemCommand="lstGalleries_ItemCommand">
            <ItemTemplate>
                <asp:LinkButton CssClass="centeredItem" ID="lnkGallery" runat="server" OnClick="lnkGallery_Click" Text='<%# Container.DataItem.ToString() %>' />
            </ItemTemplate>
        </asp:DataList>
    </p>
    <br />
    <br />
    <br />


    <asp:LinkButton ID="lnkZip" runat="server" Visible="false" OnClick="lnkZip_Click" />
    <br />
    <br />
    <asp:DataList ID="lstPics" runat="server" RepeatColumns="4" GridLines="Both" CellPadding="20" CellSpacing="20" RepeatDirection="Horizontal" CssClass="centeredItem"
        OnItemDataBound="lstPics_ItemDataBound" OnItemCommand="lstPics_ItemCommand" EnableViewState="false">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl='<%# FullURL + Container.DataItem.ToString() %>' CommandName="Click" OnCommand="ImageButton1_Command" />
            <asp:Label ID="lblalobject" runat="server" Text='<%# Container.DataItem.ToString() %>'></asp:Label>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
