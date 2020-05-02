Public Class Form1
    Dim strm As System.IO.Stream
    Dim Campaign_Num, TTP_Num As Integer
    Dim intCurrentIndex_Campaign, intCurrentIndex_TTP, intCurrentIndex_Ind, intCurrentIndex_TA, intCurrentIndex_Inc, intCurrentIndex_Ex, intCurrentIndex_Obsr As Integer

    Private Sub btnFillValues_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnStixGen_Click(sender As Object, e As EventArgs) Handles btnStixGen.Click

        Dim tmpID As Integer
        tmpID = 1

        'TODO: This line of code loads data into the 'Database1DataSet.CampaignTTPQuery' table. You can move, or remove it, as needed.
        Me.Campaign_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet1.Campaign_Query1, ComboBox1.Text)


        While (intCurrentIndex_Campaign <= Database_24_2_230DataSet1.Tables(0).Rows.Count - 1)

            tmpID = Me.Database_24_2_230DataSet1.Tables(0).Rows(intCurrentIndex_Campaign).Item("CampaignID").ToString()
            MsgBox(Me.Database_24_2_230DataSet1.Tables(0).Rows(intCurrentIndex_Campaign).Item("CampaignName").ToString())

            '*** Getting All TTPs belong to above Campaign**********************************
            Me.TTP_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet.TTP_Query1, tmpID)
            intCurrentIndex_TTP = 0
            While (intCurrentIndex_TTP <= Database_24_2_230DataSet.Tables(0).Rows.Count - 1)
                MsgBox(Me.Database_24_2_230DataSet.Tables(0).Rows(intCurrentIndex_TTP).Item("TTPName").ToString())
                intCurrentIndex_TTP += 1
            End While

            '*** Getting All Indicators belong to above Campaign**********************************
            Me.Indicators_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet2.Indicators_Query1, tmpID)
            intCurrentIndex_Ind = 0
            While (intCurrentIndex_Ind <= Database_24_2_230DataSet2.Tables(0).Rows.Count - 1)
                MsgBox(Me.Database_24_2_230DataSet2.Tables(0).Rows(intCurrentIndex_Ind).Item("IndicatorName").ToString())
                MsgBox(Me.Database_24_2_230DataSet2.Tables(0).Rows(intCurrentIndex_Ind).Item("IndicatorValue").ToString())
                intCurrentIndex_Ind += 1
            End While

            '*** Getting All Threat Actor belong to above Campaign**********************************
            Me.ThreatActor_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet3.ThreatActor_Query1, tmpID)
            intCurrentIndex_TA = 0
            While (intCurrentIndex_TA <= Database_24_2_230DataSet3.Tables(0).Rows.Count - 1)
                MsgBox(Me.Database_24_2_230DataSet3.Tables(0).Rows(intCurrentIndex_TA).Item("ThreatActorName").ToString())
                intCurrentIndex_TA += 1
            End While

            '*** Getting All Incidents belong to above Campaign**********************************
            Me.IncidentsTableAdapter.Fill(Me.Database_24_2_230DataSet4.Incidents, tmpID)
            intCurrentIndex_Inc = 0
            While (intCurrentIndex_Inc <= Database_24_2_230DataSet4.Tables(0).Rows.Count - 1)
                MsgBox("Incidents")
                MsgBox(Me.Database_24_2_230DataSet4.Tables(0).Rows(intCurrentIndex_Inc).Item("IncidentName").ToString())
                intCurrentIndex_Inc += 1
            End While


            intCurrentIndex_Campaign += 1
        End While

        MessageBox.Show("You're already at the last record.")

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim NameText As String
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Title = "Please Select a File"
        openFileDialog1.InitialDirectory = "C:temp"

        openFileDialog1.ShowDialog()
        strm = openFileDialog1.OpenFile()
        NameText = openFileDialog1.FileName.ToString()

        'NameText = "C:\\Users\\FAHAD\\Documents\\header_Info.text"

        txtRichTBViewer.Text = System.IO.File.ReadAllText(NameText)


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TODO: This line of code loads data into the 'Database_24_2_230DataSet5.Campaign' table. You can move, or remove it, as needed.
        Me.CampaignTableAdapter.Fill(Me.Database_24_2_230DataSet5.Campaign)

        Campaign_Num = 0
        TTP_Num = 0

        '*********** Campaign TTP Table Data Fetching *************************************
        intCurrentIndex_Campaign = 0
        intCurrentIndex_TTP = 0
        intCurrentIndex_Ind = 0
        intCurrentIndex_TA = 0
        intCurrentIndex_Inc = 0
        intCurrentIndex_Obsr = 0

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnTest_1_Click(sender As Object, e As EventArgs) Handles btnTest_1.Click





    End Sub

    Private Sub FillToolStripButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Campaign_Num += 1

        '****************************************************************************************
        ' Variable for Generating TTP, Incidents, Indicators etc

        Dim ttpArr(50), indArr(50), obserArr(50), incArr(50), taArr(20), obsArr(50), obserName(50), obserValue(50), expArr(50, 2) As String
        Dim ttpRun, indRun, incRun, taRun, obsRun, obsRun1, obsRun2, exRun, obserRun, tmpObserRun As Int16
        Dim presentFlg As Boolean
        ttpRun = 0
        indRun = 0
        incRun = 0
        taRun = 0
        obsRun = 0
        exRun = 0
        obsRun1 = 0
        obsRun2 = 0

        tmpObserRun = 0

        '****************************************************************************************
        Dim tmpID As Integer
        Dim Campaign_Str, TTP_Str, Ind_Name, Ind_Value, TA_Str, Inc_Name, Inc_Value, tmpString, vul_Str, Obser_Name, Obser_Value, old_Obser_Name As String
        tmpID = 1

        'TODO: This line of code loads data into the 'Database1DataSet.CampaignTTPQuery' table. You can move, or remove it, as needed.
        Me.Campaign_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet1.Campaign_Query1, ComboBox1.Text)


        txtRichTBViewer.Text += "<stix:Campaigns>" + vbCrLf

        While (intCurrentIndex_Campaign <= Database_24_2_230DataSet1.Tables(0).Rows.Count - 1)

            '&&&& Campaign &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '^^^^ Info Getting from DB ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            tmpID = Me.Database_24_2_230DataSet1.Tables(0).Rows(intCurrentIndex_Campaign).Item("CampaignID").ToString()
            Campaign_Str = Me.Database_24_2_230DataSet1.Tables(0).Rows(intCurrentIndex_Campaign).Item("CampaignName").ToString()

            '**************************************************************'
            ' Code for STIX generation of a particular Campaign    
            'If (Campaign_Str <> "TG-4127") Then
            'intCurrentIndex_Campaign += 1
            'Continue While
            'End If

            '^^^^ XML Writing ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            txtRichTBViewer.Text += "<stix:Campaign id=""apt:campaign_" + tmpID.ToString + """ xsi:Type = ""campaign:CampaignType"">" + vbCrLf
            txtRichTBViewer.Text += " <campaign:Title>" + Campaign_Str + "</campaign:Title>" + vbCrLf


            '&&&& TTPs &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All TTPs belong to above Campaign**********************************
            Me.TTP_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet.TTP_Query1, tmpID)
            intCurrentIndex_TTP = 0

            '^^^^ XML Writing ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            txtRichTBViewer.Text += " <campaign:Related_TTPs>" + vbCrLf

            While (intCurrentIndex_TTP <= Database_24_2_230DataSet.Tables(0).Rows.Count - 1)

                '^^^^ TTP info Getting from DB ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                TTP_Str = Me.Database_24_2_230DataSet.Tables(0).Rows(intCurrentIndex_TTP).Item("TTPName").ToString()

                tmpString = SpacesRemover_Fun(TTP_Str)

                txtRichTBViewer.Text += " <campaign:Related_TTP>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Relationship> Uses Malware</stixCommon:Relationship>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:TTP idref=""apt:ttp_" + tmpString + """ /> " + vbCrLf
                txtRichTBViewer.Text += "</campaign:Related_TTP>" + vbCrLf

                '** Saving TTPS ************************************
                'ttpArr(ttpRun) = "apt:ttp_" + tmpString
                presentFlg = False
                For tmpRun As Integer = 0 To ttpRun - 1
                    If (ttpArr(tmpRun) = tmpString) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    ttpArr(ttpRun) = tmpString
                    ttpRun += 1
                End If

                intCurrentIndex_TTP += 1
            End While

            '&&&& Exploit &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All Exploits belong to above Campaign**********************************
            Me.ExploitTableAdapter.Fill(Me.Database_24_2_230DataSet6.Exploit, tmpID)
            intCurrentIndex_Ex = 0
            'txtRichTBViewer.Text += "<campaign:Attribution>" + vbCrLf

            While (intCurrentIndex_Ex <= Database_24_2_230DataSet6.Tables(0).Rows.Count - 1)
                vul_Str = Me.Database_24_2_230DataSet6.Tables(0).Rows(intCurrentIndex_Ex).Item("Vulnerability").ToString()
                vul_Str = SpacesRemover_Fun(vul_Str)
                '** Saving Exploit ************************************
                presentFlg = False
                For tmpRun As Integer = 0 To taRun - 1
                    If (expArr(tmpRun, 1) = vul_Str) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    expArr(exRun, 0) = tmpID.ToString
                    expArr(exRun, 1) = vul_Str
                    exRun += 1
                End If
                intCurrentIndex_Ex += 1
            End While
            '*** Single TTP is necessary to create for Exploit , other vulnerabilities will under this TTP
            If (intCurrentIndex_Ex > 0) Then
                txtRichTBViewer.Text += "<campaign:Related_TTP>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Relationship>Exploits</stixCommon:Relationship>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:TTP idref=""apt:exploit_" + tmpID.ToString() + """/>" + vbCrLf
                txtRichTBViewer.Text += "</campaign:Related_TTP>" + vbCrLf
            End If

            txtRichTBViewer.Text += " </campaign:Related_TTPs> " + vbCrLf

            '&&&& Incidents &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All Incidents belong to above Campaign**********************************
            Me.IncidentsTableAdapter.Fill(Me.Database_24_2_230DataSet4.Incidents, tmpID)
            intCurrentIndex_Inc = 0
            txtRichTBViewer.Text += "<campaign:Related_Incidents>" + vbCrLf
            While (intCurrentIndex_Inc <= Database_24_2_230DataSet4.Tables(0).Rows.Count - 1)

                Inc_Name = Me.Database_24_2_230DataSet4.Tables(0).Rows(intCurrentIndex_Inc).Item("IncidentName").ToString()
                Inc_Value = Me.Database_24_2_230DataSet4.Tables(0).Rows(intCurrentIndex_Inc).Item("IncidentValue").ToString()

                Inc_Name = SpacesRemover_Fun(Inc_Name)
                Inc_Value = SpacesRemover_Fun(Inc_Value)

                txtRichTBViewer.Text += "<campaign:Related_Incident>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Relationship>Uses Malware</stixCommon:Relationship>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Incident idref=""apt:incident_" + Inc_Name + "-" + Inc_Value + """ />" + vbCrLf
                txtRichTBViewer.Text += "</campaign:Related_Incident>" + vbCrLf

                '** Saving Incidents ************************************
                presentFlg = False
                For tmpRun As Integer = 0 To incRun - 1
                    If (incArr(tmpRun) = Inc_Name + "-" + Inc_Value) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    incArr(incRun) = Inc_Name + "-" + Inc_Value
                    incRun += 1
                End If

                intCurrentIndex_Inc += 1

            End While
            txtRichTBViewer.Text += "</campaign:Related_Incidents>" + vbCrLf

            '&&&& Indicators &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All Indicators belong to above Campaign**********************************
            Me.Indicators_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet2.Indicators_Query1, tmpID)
            intCurrentIndex_Ind = 0

            txtRichTBViewer.Text += "<campaign:Related_Indicators>" + vbCrLf
            While (intCurrentIndex_Ind <= Database_24_2_230DataSet2.Tables(0).Rows.Count - 1)

                '^^^^ Indicators info Getting from DB ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                Ind_Name = Me.Database_24_2_230DataSet2.Tables(0).Rows(intCurrentIndex_Ind).Item("IndicatorName").ToString()
                Ind_Value = Me.Database_24_2_230DataSet2.Tables(0).Rows(intCurrentIndex_Ind).Item("IndicatorValue").ToString()

                Ind_Name = SpacesRemover_Fun(Ind_Name)
                Ind_Value = SpacesRemover_Fun(Ind_Value)

                txtRichTBViewer.Text += "<campaign:Related_Indicator>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Relationship> Uses Malware</stixCommon:Relationship>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Indicator idref=""apt:indicator_" + Ind_Name + "-" + Ind_Value + """/>" + vbCrLf
                txtRichTBViewer.Text += "</campaign:Related_Indicator>" + vbCrLf

                '** Saving Indicators ************************************
                presentFlg = False
                For tmpRun As Integer = 0 To indRun - 1
                    If (indArr(tmpRun) = Ind_Name + "-" + Ind_Value) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    indArr(indRun) = Ind_Name + "-" + Ind_Value
                    indRun += 1
                End If

                intCurrentIndex_Ind += 1

            End While
            txtRichTBViewer.Text += "</campaign:Related_Indicators>" + vbCrLf

            '&&&& Observable &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All Observable belongs to above Campaign**********************************

            txtRichTBViewer.Text += "<campaign:Related_Indicators>" + vbCrLf

            ObservableTableAdapter.Fill(Database_24_2_230DataSet7.Observable, tmpID)
            intCurrentIndex_Obsr = 0

            old_Obser_Name = ""
            While (intCurrentIndex_Obsr <= Database_24_2_230DataSet7.Tables(0).Rows.Count - 1)

                '^^^^ Indicators info Getting from DB ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                Obser_Name = Me.Database_24_2_230DataSet7.Tables(0).Rows(intCurrentIndex_Obsr).Item("ObservableName").ToString()
                Obser_Value = Me.Database_24_2_230DataSet7.Tables(0).Rows(intCurrentIndex_Obsr).Item("ObservableValue").ToString()

                Obser_Name = SpacesRemover_Fun(Obser_Name)
                Obser_Value = SpacesRemover_Fun(Obser_Value)

                If (Obser_Name <> old_Obser_Name) Then

                    txtRichTBViewer.Text += "<stixCommon:Relationship> Malware creates</stixCommon:Relationship>" + vbCrLf
                    txtRichTBViewer.Text += "<campaign:Related_Indicator>" + vbCrLf
                    txtRichTBViewer.Text += "<stixCommon:Indicator idref=""apt:indicator_" + Obser_Name + """/>" + vbCrLf
                    txtRichTBViewer.Text += "</campaign:Related_Indicator>" + vbCrLf

                    old_Obser_Name = Obser_Name
                End If

                obserName(obsRun1) = Obser_Name
                obserValue(obsRun1) = Obser_Value
                obsRun1 += 1



                '** Saving Distinct Observable ************************************
                presentFlg = False
                For tmpRun As Integer = 0 To obsRun2 - 1
                    If (obserArr(tmpRun) = Obser_Name) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    obserArr(obserRun) = Obser_Name
                    obsRun2 += 1
                End If


                intCurrentIndex_Obsr += 1

            End While

            txtRichTBViewer.Text += "</campaign:Related_Indicators>" + vbCrLf


            '--------------------------------------------

            '&&&& Threat Actor &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            '*** Getting All Threat Actor belong to above Campaign**********************************
            Me.ThreatActor_Query1TableAdapter.Fill(Me.Database_24_2_230DataSet3.ThreatActor_Query1, tmpID)
            intCurrentIndex_TA = 0
            txtRichTBViewer.Text += "<campaign:Attribution>" + vbCrLf

            While (intCurrentIndex_TA <= Database_24_2_230DataSet3.Tables(0).Rows.Count - 1)
                TA_Str = Me.Database_24_2_230DataSet3.Tables(0).Rows(intCurrentIndex_TA).Item("ThreatActorName").ToString()
                TA_Str = SpacesRemover_Fun(TA_Str)
                txtRichTBViewer.Text += "<campaign:Attributed_Threat_Actor>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Threat_Actor idref=""apt:threatactor_" + TA_Str + """ /> " + vbCrLf
                txtRichTBViewer.Text += "</campaign:Attributed_Threat_Actor>" + vbCrLf

                '** Saving ThreatActor ************************************
                presentFlg = False
                For tmpRun As Integer = 0 To taRun - 1
                    If (taArr(tmpRun) = TA_Str) Then
                        presentFlg = True
                    End If
                Next
                If (presentFlg = False) Then
                    taArr(taRun) = TA_Str
                    taRun += 1
                End If

                intCurrentIndex_TA += 1
            End While

            txtRichTBViewer.Text += "</campaign:Attribution>" + vbCrLf

            intCurrentIndex_Campaign += 1
            txtRichTBViewer.Text += "</stix:Campaign>" + vbCrLf
        End While

        MessageBox.Show("Last record has been written . . .")
        txtRichTBViewer.Text += " </stix:Campaigns>" + vbCrLf


        '** Creating TTPs **********************************************
        txtRichTBViewer.Text += "<stix:TTPs>" + vbCrLf
        For index1 As Integer = 0 To ttpRun - 1
            'MsgBox("TTP       " + ttpArr(index1))
            txtRichTBViewer.Text += "<stix:TTP id=""apt:ttp_" + ttpArr(index1) + """ xsi:Type =""ttp:TTPType"">" + vbCrLf
            txtRichTBViewer.Text += "<ttp:Title>" + ttpArr(index1) + "</ttp:Title>" + vbCrLf
            txtRichTBViewer.Text += "</stix:TTP>" + vbCrLf
        Next
        txtRichTBViewer.Text += "</stix:TTPs>" + vbCrLf

        '** Creating Incidents **********************************************
        txtRichTBViewer.Text += "<stix:Incidents>" + vbCrLf
        For index2 As Integer = 0 To incRun - 1
            'MsgBox("inc       " + incArr(index2))
            txtRichTBViewer.Text += "<stix:Incident id=""apt:incident_" + incArr(index2) + """ xsi:type='incident:IncidentType' version=""1.2"">" + vbCrLf
            txtRichTBViewer.Text += "<incident:Title>" + incArr(index2) + "</incident:Title> " + vbCrLf
            txtRichTBViewer.Text += "</stix:Incident>"
        Next
        txtRichTBViewer.Text += "</stix:Incidents>" + vbCrLf

        '** Creating Indicators **********************************************
        txtRichTBViewer.Text += "<stix:Indicators>" + vbCrLf
        For index3 As Integer = 0 To indRun - 1
            'MsgBox("ind       " + indArr(index3))
            txtRichTBViewer.Text += "<stix:Indicator id=""apt:indicator_" + indArr(index3) + """ xsi:Type = ""indicator:IndicatorType"" >" + vbCrLf
            txtRichTBViewer.Text += "<indicator:Title>" + indArr(index3) + "</indicator:Title> " + vbCrLf
            txtRichTBViewer.Text += "<indicator:Type xsi:type=""stixVocabs:IndicatorTypeVocab-1.0""> </indicator:Type>" + vbCrLf
            txtRichTBViewer.Text += "</stix:Indicator>" + vbCrLf
        Next
        txtRichTBViewer.Text += "</stix:Indicators>" + vbCrLf

        '** Creating ThreatActors **********************************************
        txtRichTBViewer.Text += "<stix:Threat_Actors>" + vbCrLf
        For index4 As Integer = 0 To taRun - 1
            'MsgBox("ta        " + taArr(index4))
            txtRichTBViewer.Text += "<stix:Threat_Actor id=""apt:threatactor_" + taArr(index4) + """ xsi:type=""ta:ThreatActorType"">" + vbCrLf
            txtRichTBViewer.Text += "<ta:Title>" + taArr(index4) + "</ta:Title>" + vbCrLf
            txtRichTBViewer.Text += ""
            txtRichTBViewer.Text += "</stix:Threat_Actor>" + vbCrLf
        Next
        txtRichTBViewer.Text += "</stix:Threat_Actors>" + vbCrLf

        '** Creating Exploits **********************************************
        Dim expTempStr As String
        Dim expFlag As Boolean
        expTempStr = ""
        expFlag = False

        txtRichTBViewer.Text += "<stix:TTPs>" + vbCrLf

        For index5 As Integer = 0 To exRun - 1
            '********************************************************************************
            If (expArr(index5, 0) <> expTempStr) Then
                If (expFlag = True) Then
                    txtRichTBViewer.Text += "</ttp:Exploit_Targets>" + vbCrLf
                    txtRichTBViewer.Text += "</stix:TTP>" + vbCrLf
                End If
                txtRichTBViewer.Text += "<stix:TTP id=""apt:exploit_" + expArr(index5, 0) + """ xsi:Type=""ttp:TTPType"">" + vbCrLf
                txtRichTBViewer.Text += "<ttp:Title> Exploit Target </ttp:Title>" + vbCrLf
                txtRichTBViewer.Text += "<ttp:Exploit_Targets>" + vbCrLf
                txtRichTBViewer.Text += "<stixCommon:Exploit_Target idref=""apt:exploit_" + expArr(index5, 1) + """/>" + vbCrLf
                expTempStr = expArr(index5, 0)
                expFlag = True
            Else
                txtRichTBViewer.Text += "<stixCommon:Exploit_Target idref=""apt:exploit_" + expArr(index5, 1) + """/>" + vbCrLf
                expTempStr = expArr(index5, 0)
            End If
        Next
        If (exRun > 0) Then
            txtRichTBViewer.Text += "</ttp:Exploit_Targets>" + vbCrLf
            txtRichTBViewer.Text += "</stix:TTP>" + vbCrLf
        End If
        txtRichTBViewer.Text += "</stix:TTPs>" + vbCrLf

        txtRichTBViewer.Text += "<stix:Exploit_Targets>" + vbCrLf
        For index6 As Integer = 0 To exRun - 1

            '********************************************************************************
            txtRichTBViewer.Text += "<stixCommon:Exploit_Target id=""apt:exploit_" + expArr(index6, 1) + """  xsi:type=""et:ExploitTargetType"">" + vbCrLf
            txtRichTBViewer.Text += "<et:Title>" + expArr(index6, 1) + "</et:Title>" + vbCrLf
            txtRichTBViewer.Text += "<et:Vulnerability>" + vbCrLf
            txtRichTBViewer.Text += "<et:CVE_ID>" + expArr(index6, 1)
            txtRichTBViewer.Text += "</et:CVE_ID>" + vbCrLf
            txtRichTBViewer.Text += "</et:Vulnerability>" + vbCrLf
            txtRichTBViewer.Text += "</stixCommon:Exploit_Target>" + vbCrLf
        Next
        txtRichTBViewer.Text += "</stix:Exploit_Targets>" + vbCrLf

        '****************************************************************************************************
        '** Creating Observable *****************************
        txtRichTBViewer.Text += "<stix:Indicators>" + vbCrLf

        old_Obser_Name = ""

        For index7 As Integer = 0 To obsRun1 - 1

            If (old_Obser_Name <> obserName(index7)) Then
                If (old_Obser_Name <> "") Then
                    txtRichTBViewer.Text += "</stix:Indicator>" + vbCrLf
                End If
                txtRichTBViewer.Text += "<stix:Indicator id=""apt:indicator_" + obserName(index7) + """ xsi:Type=""indicator:IndicatorType"">" + vbCrLf
                txtRichTBViewer.Text += "<indicator:Title>" + obserName(index7) + "</indicator:Title> " + vbCrLf
                txtRichTBViewer.Text += "<indicator:Type xsi:type=""stixVocabs:IndicatorTypeVocab-1.0""> </indicator:Type>" + vbCrLf

                txtRichTBViewer.Text += "<indicator:Observable idref=""apt:observable-" + obserName(index7) + "_" + obserValue(index7) + """/> " + vbCrLf

                old_Obser_Name = obserName(index7)
            Else
                txtRichTBViewer.Text += "<indicator:Observable idref=""apt:observable-" + obserName(index7) + "_" + obserValue(index7) + """/> " + vbCrLf
            End If

        Next
        txtRichTBViewer.Text += "</stix:Indicator>" + vbCrLf
        txtRichTBViewer.Text += "</stix:Indicators>" + vbCrLf + vbCrLf
        '****************************************************************************************************
        txtRichTBViewer.Text += "<stix:Observables cybox_major_version=""2"" cybox_minor_version=""0"" cybox_update_version=""1"">" + vbCrLf
        For index8 As Integer = 0 To obsRun1 - 1

            txtRichTBViewer.Text += "<cybox:Observable id=""apt:observable-" + obserName(index8) + "_" + obserValue(index8) + """>" + vbCrLf
            txtRichTBViewer.Text += "<cybox:Title>" + obserName(index8) + "_" + obserValue(index8) + "</cybox:Title> " + vbCrLf
            txtRichTBViewer.Text += "</cybox:Observable>" + vbCrLf + vbCrLf

        Next
        txtRichTBViewer.Text += "</cybox:Observables>"


    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub FillToolStripButton_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim box = New AboutBox1()
        box.Show()
    End Sub

    Private Sub btnAddTTPs_Click(sender As Object, e As EventArgs) Handles btnAddTTPs.Click
        Dim NameText As String
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Title = "Please Select a File"
        openFileDialog1.InitialDirectory = "C:temp"

        openFileDialog1.ShowDialog()
        strm = openFileDialog1.OpenFile()
        NameText = openFileDialog1.FileName.ToString()

        txtRichTBViewer.Text += System.IO.File.ReadAllText(NameText)
    End Sub

    Private Sub btnwriteXmlFile_Click(sender As Object, e As EventArgs) Handles btnwriteXmlFile.Click
        txtRichTBViewer.Text += "</stix:STIX_Package>" + vbCrLf
        txtRichTBViewer.Text += ("'   Alhamdulillah") + vbCrLf
        '****************************************************************************************
        Dim pathStr As String
        pathStr = "E:\\PhD Corse Work\\8     PhD Research\\6_____Malware\\0____Paper\\2______Second Paper\\10_____STIX Visulization\APT - POS\\0_______" + ComboBox1.Text + "_APTS_STIX.xml"

        System.IO.File.WriteAllText(pathStr, txtRichTBViewer.Text)
        MsgBox("Info : File has been generated . . .")
    End Sub

    Private Sub btnClearRichTextBox_Click(sender As Object, e As EventArgs) Handles btnClearRichTextBox.Click
        txtRichTBViewer.Clear()
    End Sub

    Function SpacesRemover_Fun(ByVal tempStr As String) As String
        Dim newString As String

        newString = ""
        For Each c As Char In tempStr
            If (c = " ") Then
            Else
                newString += c
            End If
        Next
        Return newString
    End Function
    Function StringSorter_Fun(ByRef tempStr() As String, ByVal len As Integer) As String
        Dim newString As String

        newString = ""
        For index1 As Integer = 1 To len
            For index2 As Integer = index1 To len
                If (tempStr(index1) = tempStr(index2)) Then
                    For index3 As Integer = index2 To len
                        tempStr(index2) = tempStr(index3)
                    Next
                    len = len - 1
                End If
            Next
        Next
        Return newString
    End Function

End Class
