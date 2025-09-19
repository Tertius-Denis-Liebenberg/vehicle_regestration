<%@ Page Title="Contractor Vehicles" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="frmContractor.aspx.cs" Inherits="vehicle_regestration.Pages.Contractor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Contractor Management
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="mb-4">Contractor Vehicles</h2>

        <div class="row mb-3 d-flex align-items-end justify-content-between">
            <div class="col-md-6 d-flex">
                <label for="ddlContractors" class="form-label pt-2">Select Contractor:</label>
                <asp:DropDownList ID="ddlContractors" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlContractors_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addContractorModal">
                    Add New Contractor
                </button>
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvVehicles" runat="server" CssClass="table table-striped table-bordered mt-3" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TYPE" HeaderText="Type" />
                    <asp:BoundField DataField="REGESTRATION_NUMBER" HeaderText="Registration Number" />
                    <asp:BoundField DataField="MODEL" HeaderText="Model" />
                    <asp:BoundField DataField="WEIGHT" HeaderText="Weight (tons)" />
                </Columns>
            </asp:GridView>

        </div>

        <div class="modal fade" id="addContractorModal" tabindex="-1" aria-labelledby="addContractorModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="addContractorModalLabel">Add New Contractor</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form id="formAddContractor">
                            <div class="mb-3">
                                <label for="txtContractorName" class="form-label">Contractor Name</label>
                                <input type="text" class="form-control" id="txtContractorName" runat="server" />
                            </div>
                            <div class="mb-3">
                                <label for="txtContractorNumber" class="form-label">Contact Number</label>
                                <input type="tel" class="form-control" id="txtContractorNumber" runat="server" />
                            </div>
                            <div class="mb-3">
                                <label for="txtContractorEmail" class="form-label">Email</label>
                                <input type="email" class="form-control" id="txtContractorEmail" runat="server" />
                            </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveContractor" runat="server" CssClass="btn btn-primary" Text="Save Contractor" OnClick="btnSaveContractor_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
