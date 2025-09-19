<%@ Page Title="Vehicle Registration" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="frmVehicles.aspx.cs" Inherits="vehicle_regestration.Pages.frmVehicles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vehicle Registration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Vehicle Management</h2>

        <button type="button" class="btn btn-primary mb-3" data-bs-toggle="modal" data-bs-target="#vehicleModal" onclick="clearVehicleModal();">
            Add New Vehicle
        </button>


        <asp:GridView ID="gvVehicles" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" OnRowCommand="gvVehicles_RowCommand">
            <Columns>
                <asp:BoundField DataField="TYPE" HeaderText="Type" />
                <asp:BoundField DataField="REGESTRATION_NUMBER" HeaderText="Registration Number" />
                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                <asp:BoundField DataField="WEIGHT" HeaderText="Weight (tons)" DataFormatString="{0:N2}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server"
                            CommandName="EditVehicle"
                            CommandArgument='<%# Eval("ENTRY_ID") %>'
                            CssClass="btn btn-sm btn-warning me-1">Edit</asp:LinkButton>

                        <asp:LinkButton ID="btnDelete" runat="server"
                            CommandName="DeleteVehicle"
                            CommandArgument='<%# Eval("ENTRY_ID") %>'
                            CssClass="btn btn-sm btn-danger">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="modal fade" id="vehicleModal" tabindex="-1" aria-labelledby="vehicleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="vehicleModalLabel">Add/Edit Vehicle</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfVehicleID" runat="server" />

                    <div class="mb-3">
                        <label for="txtType" class="form-label">Type</label>
                        <input type="text" class="form-control" id="txtType" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label for="txtRegNo" class="form-label">Registration Number</label>
                        <input type="text" class="form-control" id="txtRegNo" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label for="txtModel" class="form-label">Model</label>
                        <input type="text" class="form-control" id="txtModel" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label for="txtWeight" class="form-label">Weight (tons)</label>
                        <input type="number" step="0.01" class="form-control" id="txtWeight" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label for="ddlContractor" class="form-label">Contractor</label>
                        <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveVehicle" runat="server" Text="Save Vehicle" CssClass="btn btn-primary" OnClick="btnSaveVehicle_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function clearVehicleModal() {
            document.getElementById('<%= hfVehicleID.ClientID %>').value = '';
        document.getElementById('<%= txtType.ClientID %>').value = '';
        document.getElementById('<%= txtRegNo.ClientID %>').value = '';
        document.getElementById('<%= txtModel.ClientID %>').value = '';
        document.getElementById('<%= txtWeight.ClientID %>').value = '';
        document.getElementById('<%= ddlContractor.ClientID %>').selectedIndex = 0;
        }
    </script>
</asp:Content>
