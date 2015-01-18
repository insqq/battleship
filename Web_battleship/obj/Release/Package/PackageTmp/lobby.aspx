<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lobby.aspx.cs" Inherits="Web_battleship.lobby" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    
    <script src="JScripts/JS_Ships.js" type="text/javascript"> </script>
    <script src="JScripts/JS_keypress.js" type="text/javascript"> </script>

</head>
<body onload="initShips(); script_ready_button();">
    <form id="form1" runat="server">
        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="2000" OnTick="Timer1_Tick"></asp:Timer>
                <asp:Timer ID="Timer2" runat="server" Enabled="False" Interval="2000" OnTick="Timer2_Tick"></asp:Timer>
                <asp:Timer ID="Timer_Chat" runat="server" Enabled="True" Interval="2000" OnTick="Timer_Chat_Tick">
                </asp:Timer>
                <asp:Timer ID="Timer_WaitingPlayer" runat="server" Interval="1000" OnTick="Timer_WaitingPlayer_Tick" Enabled="False"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="height: 38px;">
            <asp:UpdatePanel ID="UpdatePanel_status" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="Button_logout" runat="server" Height="25px" OnClick="Button_logout_Click" Text="Logout" Width="57px" UseSubmitBehavior="False" />
                    <asp:Label ID="Label_name" runat="server" Font-Bold="True" Font-Names="Lucida Handwriting" ForeColor="#33CC33" Style="float: none"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label_statusTop" runat="server" Text="status" Font-Bold="True" Font-Names="Lucida Handwriting" ForeColor="#33CC33" Font-Size="20"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>




        <div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:Button ID="Button_findGame" runat="server" OnClick="Button_findGame_Click" Text="Find Game" Height="26px" Width="113px" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="height: 211px;">

            <div style="height: 231px; width: 211px; float: left;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        <asp:AsyncPostBackTrigger ControlID="Timer_WaitingPlayer" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="height: 231px; width: 211px; float: left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Enabled="False">
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer_WaitingPlayer" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>

                <div>
                    <asp:UpdatePanel ID="UpdatePanel_EnemyNickname" runat="server" Visible="False">
                        <ContentTemplate>
                            <asp:Label ID="Label_EnemyNickname" runat="server" Font-Bold="True" Font-Names="Candara" ForeColor="#33CC33" Font-Size="15pt"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>

            <div style="height: 208px; width: 211px; float: left;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button Width="25px" Height="20" OnClientClick="event_pick_ship(one)" ID="Button_ship_size1" runat="server" Text="x4" OnClick="Button_pickShipClick" />
                        <br />
                        <br />
                        <asp:Button Width="40" Height="20" OnClientClick="event_pick_ship(two)" ID="Button_ship_size2" runat="server" Text="x3" OnClick="Button_pickShipClick" />
                        <br />
                        <br />
                        <asp:Button Width="60" Height="20" OnClientClick="event_pick_ship(three)" ID="Button_ship_size3" runat="server" Text="x2" OnClick="Button_pickShipClick" />
                        <br />
                        <br />
                        <asp:Button Width="80" Height="20" OnClientClick="event_pick_ship(four)" ID="Button_ship_size4" runat="server" Text="x1" OnClick="Button_pickShipClick" />
                        <br />
                        <asp:ImageButton ID="ImageButton_Wrap" runat="server" Height="50px" Width="50px" ImageUrl="~/Button_wrap.jpg" OnClientClick="event_Wrap_ship();" OnClick="ImageButton_Wrap_Click" />

                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>

            <div>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Label_Waiting" runat="server" Text="Waiting For Another Player..." Font-Bold="True" Font-Names="Lucida Handwriting" ForeColor="#33CC33" Font-Size="20"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer_WaitingPlayer" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </div>


        <br />
        <br />
        <div>
            <asp:UpdatePanel ID="UpdatePanel_Ready" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ImageButton ID="Button_Ready" runat="server" ImageUrl="~/Button_ready.jpg" OnClick="Button_Ready_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel_chat" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel_Chat" runat="server" BackColor="#CCCCCC" Height="300px" Width="508px">
                        <asp:Label ID="TextBox_Chat" runat="server"></asp:Label>
                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer_Chat" EventName="Tick" />
                </Triggers>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer_WaitingPlayer" EventName="Tick" />
                </Triggers>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_chat1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox_chatinput" runat="server" Height="28px" Width="440px" BackColor="#FFFFCC"></asp:TextBox>
                    <asp:Button ID="Button_chatEnter" runat="server" OnClick="Button_chatEnter_Click" Text="Enter" Width="60px" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


    </form>
</body>
</html>
