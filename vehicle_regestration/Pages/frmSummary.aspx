<%@ Page Title="Summary" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="frmSummary.aspx.cs" Inherits="vehicle_regestration.Pages.frmSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Contractor Vehicle Summary
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Contractor Vehicle Summary</h2>

        <div class="table-responsive">
            <asp:GridView ID="gvSummary" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CONTRACTOR_NAME" HeaderText="Contractor" />
                    <asp:BoundField DataField="TOTAL_VEHICLES" HeaderText="No of Vehicles" />
                    <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Total Tons" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
