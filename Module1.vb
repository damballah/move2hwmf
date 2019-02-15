Module Module1

    Public Sub Main(ByVal Args() As String)
        Dim nArgsMax As Integer = 0
        Dim i, j As Integer
        Dim lerreur, messCtrl1, messCtrl2, ArgsInOne As String
        Dim IsLog As Boolean

        nArgsMax = Args.Length
        i = 0
        j = 0
        ArgsInOne = ""

        Console.WriteLine("")

        'console.WriteLine(nArgsMax)

        If nArgsMax = 0 Then
            cmd_help()
            GoTo fin3

        Else

            If nArgsMax < 4 Then

                While j <> nArgsMax

                    ArgsInOne = ArgsInOne + "argument" + Str(j) + ":" + Chr(34) + Args(j) + Chr(34) + "; "
                    j = j + 1

                End While

                lerreur = DateAndTime.Now + " Err d'argument où nombre d'arguments insuffisants (Min.4) : " + ArgsInOne
                If Args(1) = "-log=y" Then WriteLog(lerreur)
                Console.WriteLine(lerreur)
                GoTo fin2
            Else

                If ctrl_arg(Args(0), "-dwc=y", "-dwc=n") = True Then
                    messCtrl1 = "Argument 1 ok = " + Args(0)

                    'Console.WriteLine(messCtrl1)
                Else
                    messCtrl1 = DateAndTime.Now + " Argument 1 mauvais ou absent = " + Args(0)
                    Call WriteLog(messCtrl1)
                    'Console.WriteLine(messCtrl1)
                    GoTo fin2
                End If

                If ctrl_arg(Args(1), "-log=y", "-log=n") = True Then
                    messCtrl2 = "Argument 2 ok = " + Args(1)
                    If Args(1) = "-log=y" Then IsLog = True Else IsLog = False
                    'Console.WriteLine(messCtrl2)
                Else
                    messCtrl2 = DateAndTime.Now + " Argument 2 mauvais ou absent = " + Args(1)
                    If Args(1) = "-log=y" Then Call WriteLog(messCtrl2)
                    Console.WriteLine(messCtrl2)
                    IsLog = False
                    GoTo fin2
                End If

            End If

        End If

suite:
        i = 3
        While i <> nArgsMax
            If Copy2Dir(Args(2), Args(i), IsLog) = True Then
                lerreur = DateAndTime.Now + " *** CONGRAT *** " + Args(2) + " copié vers " + Args(i)
                If IsLog = True Then Call WriteLog(lerreur)
                Console.WriteLine(lerreur)
                i = i + 1
            Else
                i = i + 1
            End If
        End While

        i = 0

        GoTo fin2

fin2:
        If Args(0) = "-dwc=y" Then
            Kill(Args(2))
            lerreur = "Le fichier source : " + Chr(34) + Args(2) + Chr(34) + " à été supprimer (instruction '-dwc=y' en param.)"
            Console.WriteLine(lerreur)
        Else

        End If
fin3:

    End Sub

    Function recupfilename(ByVal file As String) As String
        Dim reponse As String
        Dim n As Integer
        n = file.LastIndexOf("\")
        reponse = Right(file, Len(file) - n - 1)
        Return reponse
    End Function

    Function Copy2Dir(ByVal file As String, ByVal folder As String, ByVal ValLog As Boolean) As Boolean
        Dim lerreur, FolderAndNameFile As String
        On Error GoTo erreur

        If FileLen(file) < 1 Then GoTo erreur_taille

        FolderAndNameFile = folder + "\" + recupfilename(file)
        FileCopy(file, FolderAndNameFile)
        Copy2Dir = True
        GoTo fin

erreur:
        Copy2Dir = False
        lerreur = DateAndTime.Now + " Err n°" + Str(Err.Number) + " Descrip : " + Err.Description + " Source : " + Err.Source + " /// demande /// --> '" + file + "' à copier vers : '" + folder + "'"
        Console.WriteLine(lerreur)
        If ValLog = True Then Call WriteLog(lerreur)
        GoTo fin

erreur_taille:
        Copy2Dir = False
        lerreur = DateAndTime.Now + " Err interne : la taille du fichier " + Chr(34) + file + Chr(34) + " est inférieur à 1 octet, le fichier est vide !"
        Console.WriteLine(lerreur)
        If ValLog = True Then Call WriteLog(lerreur)
        GoTo fin
fin:

    End Function

    Public Function ctrl_arg(ByVal vAlarg As String, ByVal vAlRecherche1 As String, ByVal vAlRecherche2 As String) As Boolean
        Dim reponseBool As Boolean
        If vAlarg = vAlRecherche1 Or vAlarg = vAlRecherche2 Then
            reponseBool = True
        Else
            reponseBool = False
        End If
        Return reponseBool
    End Function

    Public Sub WriteLog(ByVal MessageLog As String)
        Dim FichierLog, ladateheure As String
        ladateheure = DDay()
        FichierLog = CurDir() + "\" + "move2hwmf_log_" + ladateheure + ".txt"

        FileOpen(1, FichierLog, OpenMode.Append)
        PrintLine(1, MessageLog)
        FileClose(1)

    End Sub

    Public Function DDay() As String
        Dim LadateHeureDuJour, annee, mois, jour As String

        annee = DateAndTime.Year(Now)
        mois = DateAndTime.Month(Now)
        If Len(mois) = 1 Then mois = "0" + mois

        jour = DateAndTime.Day(Now)
        If Len(jour) = 1 Then mois = "0" + mois

        LadateHeureDuJour = jour + mois + annee

        Return LadateHeureDuJour

    End Function


    Public Sub cmd_help()
        Dim L0, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11, L12, L13, L14, L15, L16, L17, L18, L19, L20, L21, L22 As String
        L0 = ""
        L1 = "MOVE2HWMF - [M]ove [to] [H]ello [W]orld [M]other [F]ucker"
        L2 = "--------------------"
        L3 = "| AIDE SUR LES CMD |"
        L4 = "--------------------"
        L5 = ""
        L6 = "Usage : move2hwmf + [spc] + '-dwc=[y/n]' + [spc] + '-log=[y/n]' + [spc] + 'source_file' + [spc] + 'dossier_de_destination1'"
        L7 = "+ [spc] + dossier_de_destination2 (recommencer la dernière opération pour copier le fichier dans autant de dossiers souhaités)"
        L8 = ""
        L9 = "Arguments obligatoires"
        L10 = "----------------------"
        L11 = "#1 [-dwc=y] où [-dwc=n] ---> (del when copied) permet de choisir si le fichier source est à supprimer où non après la copie"
        L12 = "#2 [-log=y] où [-log=n] ---> permet de créer un fichier de log où non : 'move2hwmf_log_date_du_jour.txt' dans le dossier du programme"
        L13 = "#3 [fichier_source]    ---> saisir le chemin du fichier source (jusqu'au fichier source lui-même)"
        L14 = "#4 [dossier_de_destination1] ---> saisir le chemin du dossier de destination jusqu'à '\' (inclus)"
        L15 = "#5 [dossier_de_destination2] ---> saisir un second dossier de destination, voir un troisième où même plus en suivant la même méthode"
        L16 = ""
        L17 = "Exemple : move2hwmf -dwc=y -log=y c:\dossier\filetest.txt c:\dossier2 c:\dossier3 c:\dossier4"
        L18 = ""
        L19 = "--------------------"
        L20 = "|   FIN DE L'AIDE  |"
        L21 = "--------------------"
        L22 = ""

        Console.WriteLine(L0)
        Console.WriteLine(L1)
        Console.WriteLine(L2)
        Console.WriteLine(L3)
        Console.WriteLine(L4)
        Console.WriteLine(L5)
        Console.WriteLine(L6)
        Console.WriteLine(L7)
        Console.WriteLine(L8)
        Console.WriteLine(L9)
        Console.WriteLine(L10)
        Console.WriteLine(L11)
        Console.WriteLine(L12)
        Console.WriteLine(L13)
        Console.WriteLine(L14)
        Console.WriteLine(L15)
        Console.WriteLine(L16)
        Console.WriteLine(L17)
        Console.WriteLine(L18)
        Console.WriteLine(L19)
        Console.WriteLine(L20)
        Console.WriteLine(L21)
        Console.WriteLine(L22)

    End Sub


End Module