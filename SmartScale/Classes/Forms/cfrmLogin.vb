Option Explicit On

Public Class cfrmLogin
    Private mobjLogin As clsLogin
    Private mshrtFrmheight As Short
    Private mObjMain As cfrmMain
    
#Region "Form"
    Private Sub cfrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        mshrtFrmheight = Me.Height
        gUserPic = PicBoxUser.Image
        Me.Height = gMethods.FrmResize(mshrtFrmheight, pnlChangePassword.Height, clsMainMethods.FrmHeight.ControlHeight)
        ' gAdo.CtrlItemsLoad("User_ID", "User_Name", "tblUser", cboEmpName)
        gAdo.CtrlItemsLoad("User_ID", "User_NickName", "tblUser", cboEmpName, "", "User_IsDeleted='false' and User_ID<>1 ORDER BY User_Name")

        cboEmpName.Text = ""
        'cboEmpName.Text = "Midooo"
        'btnLogin_Click(sender, e)
    End Sub

    Private Sub cfrmLogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        mobjLogin = Nothing
    End Sub
#End Region

#Region "Login"
    Public Property Login() As clsLogin
        Get
            Return mobjLogin
        End Get
        Set(ByVal value As clsLogin)
            mobjLogin = value
        End Set
    End Property

    Private ReadOnly Property IsPasswordValid() As Boolean
        Get
            'If cboEmpName.Text.Trim.Length = 0 Then
            '    Call cboEmpName.Focus()
            '    Exit Property
            'End If

            'If txtPassword.Text.Trim.Length = 0 Then
            '    Call txtPassword.Focus()
            '    Call txtPassword.SelectAll()
            '    Exit Property
            'End If

            'If cboEmpName.Items.Count = 0 Then
            '    gAdo.CmdExec("Insert into TblUser(User_Name,User_Password,User_IsDeleted) values('" _
            '                 & cboEmpName.Text.Trim & "','" & txtPassword.Text.Trim & "','false')", False)
            '    ' gUserID = gAdo.CmdExecScalar("select max(User_id) from tbluser")
            '    gUserID = gAdo.GetMax("User_ID", "tbluser")

            Dim UserCount As Integer
            UserCount = gAdo.CmdExecScalar("Select count (User_ID) from TblUser")

            If cboEmpName.Text.Trim = "" And txtPassword.Text.Trim = "" And UserCount = 1 Then
                GoTo Ret
            End If


            If gLogin.F_Login(cboEmpName.Text, Trim(txtPassword.Text)) = Trim(txtPassword.Text) Then

                gUserID = gAdo.CmdExecScalar("select User_ID from TblUser where User_NickName='" & cboEmpName.Text & "'")
                Dim FirstShiftST, FirstShiftED As String

                FirstShiftST = gAdo.CmdExecScalar("select Shift_Start_Time from tblShift where Shift_ID=1")
                FirstShiftED = gAdo.CmdExecScalar("select Shift_End_Time from  tblShift where Shift_ID=1")
                If Now.TimeOfDay > Convert.ToDateTime(FirstShiftST).TimeOfDay _
                And Now.TimeOfDay < Convert.ToDateTime(FirstShiftED).TimeOfDay Then
                    ShiftID = 1
                Else
                    ShiftID = 2
                End If

            ElseIf gLogin.F_Login(cboEmpName.Text, Trim(txtPassword.Text)) = "Error Name" Then
                Call MsgBox("أسم الموظف غير صحيح", MsgBoxStyle.Critical)
                Call cboEmpName.Focus()
                Call cboEmpName.SelectAll()
                Exit Property
            ElseIf gLogin.F_Login(cboEmpName.Text, Trim(txtPassword.Text)) = "Error Password" Then
                Call MsgBox("كلمة السر غير صحيحة", MsgBoxStyle.Critical)
                Call txtPassword.Focus()
                Call txtPassword.SelectAll()
                Exit Property
            End If

Ret:
            Return True
        End Get
    End Property

    Private ReadOnly Property IsNewPasswordValid() As Boolean
        Get
            If txtNewPassword.Text.Trim.Length = 0 Then
                Call txtNewPassword.Focus()
                Call txtNewPassword.SelectAll()
                Exit Property
            End If

            If txtNewPasswordConfirmation.Text.Trim.Length = 0 Then
                Call txtNewPasswordConfirmation.Focus()
                Call txtNewPasswordConfirmation.SelectAll()
                Exit Property
            End If

            If txtNewPassword.Text.Trim().ToLower() <> txtNewPasswordConfirmation.Text.Trim().ToLower() Then
                Call MsgBox("كلمة السر الجديدة و تأكيدها لا يتطابقان", MsgBoxStyle.Critical)
                Call txtNewPassword.Focus()
                Call txtNewPassword.SelectAll()
                Exit Property
            End If

            Return True
        End Get
    End Property

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Not IsPasswordValid Then Exit Sub
        'Me.Login.LoginTime = Now
        ' gUserID = cboEmpName.SelectedValue

        'gUserID = gAdo.CmdExecScalar("select User_ID from TblUser where User_name='" & cboEmpName.Text & "'")
        'cboEmpName.SelectedIndex = cboEmpName.FindString(cboEmpName.Text)
        'gUserID = cboEmpName.SelectedValue
        '  MessageBox.Show(gUserID)
        gUserName = cboEmpName.Text
        gUserPic = PicBoxUser.Image
        gLoginTime = Now
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region

#Region "Controls"
    Private Sub lnkChangePassword_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkChangePassword.LinkClicked
        On Error Resume Next
        If Not IsPasswordValid Then Exit Sub
        lblMsg.Text = "تم التأكد من كلمه السر ، برجاء ادخال كلمه السر الجديده وتأكيدها لضمان نجاح تغيير كلمه السر"
        pnlChangePassword.Visible = True
        pnlPassword.Visible = False
        pnlChangePassword.Top = pnlPassword.Top
        Me.AcceptButton = btnChangePassword

        Me.Height = gMethods.FrmResize(mshrtFrmheight, pnlPassword.Height, clsMainMethods.FrmHeight.ControlHeight)
    End Sub

    Private Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangePassword.Click
        On Error Resume Next
        If Not IsNewPasswordValid Then Exit Sub

        Dim newPass As String = txtNewPassword.Text.Trim
        If gLogin.ChangePassword(cboEmpName.SelectedValue, txtNewPassword.Text.Trim) = True Then
            lblMsg.Text = "تم تغيير كلمه السر ، برجاء ادخال كلمه السر الجديده لكى تتمكن من تسجيل الدخول"
            gMethods.ClearObj(Me, "TextBox")
            txtPassword.Text = newPass
        Else
            MsgBox("لم يتم تغيير كلمه السر ، من فضلك أعد المحاوله")
            Exit Sub
        End If
        pnlChangePassword.Visible = False
        pnlPassword.Visible = True
        Me.Height = gMethods.FrmResize(mshrtFrmheight, pnlChangePassword.Height, clsMainMethods.FrmHeight.ControlHeight)

        'Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cboEmpName_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpName.SelectedValueChanged
        'On Error Resume Next
        'lblMsg.Text = "برجاء إختيار اسمك من قائمة إسم الموظف  ثم كتابة كلمة السر لكي تتمكن من تسجيل الدخول"

        'gMethods.BinaryPicLoad("Picture", "tblPicture", _
        '                      "pic_id=(select User_Pic_ID from tblUser where User_IsDeleted=0 And [User_Id]=" & _
        '                       cboEmpName.SelectedValue & ")", PicBoxUser)
    End Sub
#End Region

    Private Sub cboEmpName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpName.TextChanged
        On Error Resume Next

        Dim L As InputLanguage

        Dim i As Integer = 0
        For Each L In InputLanguage.InstalledInputLanguages
            i += 1
            If L.Culture.EnglishName = "Arabic (Egypt)" Then
                System.Windows.Forms.InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages(i - 1)
            Else

            End If

        Next

        lblMsg.Text = "برجاء إختيار اسمك من قائمة إسم الموظف  ثم كتابة كلمة السر لكي تتمكن من تسجيل الدخول"

        If gMethods.BinaryPicLoad("Picture", "tblPicture", _
                               "pic_id=(select User_Pic_ID from tblUser where User_IsDeleted=0 And [User_Nickname]='" & _
                                cboEmpName.Text & "')", PicBoxUser) = False Then

            PicBoxUser.Image = gUserPic
        End If
    End Sub

    Private Sub cboEmpName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboEmpName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown _
    , txtPassword.KeyDown, txtNewPasswordConfirmation.KeyDown
        If e.KeyCode = Keys.Enter Then
            ProcessTabKey(True)
        End If
    End Sub

    Private Sub btnLogin_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.MouseEnter, btnChangePassword.MouseEnter
        sender.BackColor = Color.LightSeaGreen
    End Sub

    Private Sub btnLogin_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.MouseLeave, btnChangePassword.MouseLeave
        sender.BackColor = Color.AliceBlue
    End Sub

End Class