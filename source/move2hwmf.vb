Module move2hwmf

    Public Sub Main(ByVal Args() As String)
        'Déclaration des variables
        '-------------------------
        Dim nArgsMax As Integer = 0
        'var destinée à récupérer le nombre d'arguments total passés en paramètres

        Dim i, j As Integer
        'var de comptages simples dans les boucles "while"

        Dim lerreur, messCtrl1, messCtrl2, ArgsInOne As String
        'var lerreur, messCtrl1 et messCtrl2 = messages d'erreurs générés quand une erreur se produit (incrit au log quand l'option est activé)
        'var ArgsInOne = dans la boucle while, récupère tous les arguments passés en paramètres.

        Dim IsLog As Boolean
        'var booléen contrôlant si le paramètre '-log' est bien paramétré sur y ou n. Ex: '-log=y' où 'log=n'

        nArgsMax = Args.Length
        'affectation de la valeur du nombre d'arguments total passés en paramètre

        i = 0
        'affectation de la variable i à 0
        j = 0
        'affectation de la variable j à 0
        ArgsInOne = ""
        'affectation de la variable ArgsInOne à rien (vide)

        Console.WriteLine("")
        'Affichage dans la console de commande (dos) d'une ligne vide (retour chariot)


        'Explication de la condition suivante :'
        '**************************************'
        '#1 [Si] le nombre d'arguments total passés en paramètre est égal à 0,
        '       [Alors] lancer le sous programme d'affichage de l'aide du programme'
        '       et aller à l'étiquette "fin3" afin d'executer le code s'y trouvant.
        '       En sachant que l'étiquette "fin3" ne contient aucun code, fin du programme, retour à l'invite de commande'
        '   [Sinon] (si le nombre d'arguments n'est pas égal à 0, sous entendu supérieur à 0)
        '   Lancer une seconde condition (#2)

        '#1'
        If nArgsMax = 0 Then
            cmd_help()
            GoTo fin3

        Else
            '#2 [Si] le nombre total d'arguments est inférieur à 4,
            '       [Alors] créer une boucle avec la var 'j' de comptage : tant que 'j' est différent du nombre total d'arguments:
            '           Création d'une phrase avec insertion au fur et à mesure de l'argument numéro 'j'
            '           
            If nArgsMax < 4 Then

                While j <> nArgsMax

                    ArgsInOne = ArgsInOne + "argument" + Str(j) + ":" + Chr(34) + Args(j) + Chr(34) + "; "
                    j = j + 1

                End While
                '           Création de la phrase d'erreur

                lerreur = DateAndTime.Now + " Err d'argument où nombre d'arguments insufisants (Min.4) : " + ArgsInOne

                'Si l'argument 2 (numéro 1) est égal à '-log=y' alors nous écrivons la phrase dans le fichier log et nous l'affichons aussi
                If Args(1) = "-log=y" Then WriteLog(lerreur)
                Console.WriteLine(lerreur)
                GoTo fin2
            Else
                '[Sinon]
                'Si l'argument 1 (numéro 0) est égal à '-dwc=y' où '-dwc=n' alors 
                If ctrl_arg(Args(0), "-dwc=y", "-dwc=n") = True Then
                    'la variable d'erreur est égale à : Argument 1 ok + (l'argument 1)
                    messCtrl1 = "Argument 1 ok = " + Args(0)
                Else
                    '[Sinon]
                    messCtrl1 = DateAndTime.Now + " Argument 1 mauvais ou absent = " + Args(0)
                    'la variable d'erreur est égale à : Argument 1 mauvais ou absent + (l'argument 1)
                    'puis l'écrire dans le log (toujours si le param -log=y)
                    Call WriteLog(messCtrl1)

                    'aller à l'étiquette fin2
                    GoTo fin2

                End If

                '[Si] le contrôle d'argument n°2 '-log' est à 'y' où à 'n' : 
                If ctrl_arg(Args(1), "-log=y", "-log=n") = True Then
                    '[Alors] le message d'erreur est : Argument 2 ok + l'argument 2 (n°1)
                    messCtrl2 = "Argument 2 ok = " + Args(1)

                    '[Si] l'argument n°2 (1), -log=y [Alors] la var IsLog (booléen) est à Vrai [Sinon] IsLog est à Faux
                    If Args(1) = "-log=y" Then IsLog = True Else IsLog = False

                Else
                    '[Sinon] 
                    'messCtrl est égale à : Argument 2 mauvais ou absent + l'argument 2 (1)
                    messCtrl2 = DateAndTime.Now + " Argument 2 mauvais ou absent = " + Args(1)

                    If Args(1) = "-log=y" Then Call WriteLog(messCtrl2)
                    '[Si] l'argument 2 (1) est égale à -log=y' alors nous écrivons le messCtrl2 dans le log,
                    'Quoi qu'il arrive nous affichons dans la console (dos) le message messCtrl2
                    Console.WriteLine(messCtrl2)

                    'Nous rétablissons la valeur de IsLog à Faux
                    IsLog = False

                    'Nous pointons le programme pour executer l'étiquette 'fin2'
                    GoTo fin2
                    'Fin de la condition 3
                End If
                'Fin de la condition 2
            End If
            'fin de la condition 1
        End If


        'Etiquette suite (validation du lancement de la copie du fichie source vers le où les dossiers passés en paramètres'
        '*******************************************************************************************************************'
suite:
        i = 3
        'Initialisation de la var i à 3 (à supposer que tous les tests des 3 premiers arguments sont OK)
        While i <> nArgsMax
            'Tant que la valeur de i est différente du nombre d'arguments total 

            If Copy2Dir(Args(2), Args(i), IsLog) = True Then
                '[Si] booléen de la fonction Copy2Dir de l'argument n°2 (le fichier source), vers le dossier de destination (i) et la valeur de IsLog donne la valeur VRAI 

                lerreur = DateAndTime.Now + " *** CONGRAT *** " + Args(2) + " copié vers " + Args(i)
                'Creation de la phrase lerreur (ce n'est que le nom de la variable, ce n'est peut être pas une phrase concernant une erreur)
                '= *** CONGRAT *** fichier.ext copié vers dossier(i)'

                If IsLog = True Then Call WriteLog(lerreur)
                '[Si] valeur de IsLog=VRAI [Alors] Appel du sous programme WriteLog(lerreur)
                'fin de la condition 2

                Console.WriteLine(lerreur)
                'Affichage de la phrase (lerreur) dans la console (dos)

                i = i + 1
                'auto incrémentation de i (valeur de i= valeur de i + 1)

            Else
                '[Sinon]
                i = i + 1
                'auto incrémentation de i seulement (valeur de i= valeur de i + 1)

            End If
            'Fin de la condition 1

        End While

        i = 0

        GoTo fin2

fin2:
        '[Si] le paramètre dwc (del when copied, si nous voulons supprimer le ficier source après la copie) est sur 'y'
        If Args(0) = "-dwc=y" Then
            'Supprimer le fichier source (argument n°3 de la commande)
            Kill(Args(2))
            'Générer la phrase confirmant la suppression du fichier source (la variable 'lerreur' n'est qu'un contenant)
            lerreur = "Le fichier source : " + Chr(34) + Args(2) + Chr(34) + " à été supprimer (instruction '-dwc=y' en param.)"
            'Afficher la phrase dans la console (dos)
            Console.WriteLine(lerreur)
        Else
            '[Sinon] ne rien faire, sous entendu, ne pas supprimer le fichier source en paramètre 3
        End If
fin3:

    End Sub

    Function recupfilename(ByVal file As String) As String
        'Déclaration des variables
        Dim reponse As String 'reponse est une chaine
        Dim n As Integer 'n est un nombre entier

        'récupération de la position dans la chaîne contenant le chemin du fichier à copier, de la position du dernier "\"
        n = file.LastIndexOf("\")

        'reponse (nom de fichier seul, sans le chemin complet) = droite de la chaine contenant le chemin du fichier à copier, longueur du chemin complet, (moins la valeur de n moins 1)
        reponse = Right(file, Len(file) - n - 1)

        'la fonction recupfilename contient alors la variable "reponse" (return reponse)
        Return reponse
    End Function

    Function Copy2Dir(ByVal file As String, ByVal folder As String, ByVal ValLog As Boolean) As Boolean
        'Déclaration des variables
        Dim lerreur, FolderAndNameFile As String 'lerreur, FolderAndNameFile sont des chaînes de caractères
        On Error GoTo erreur 'Pour toute erreur, aller à l'étiquette erreur

        '[Si] la taille du fichier à copier est inférieur à 1 octet (donc vide), [Alors] aller à l'étiquette erreur_taille
        If FileLen(file) < 1 Then GoTo erreur_taille

        'La variable FolderAndNameFile est égale à la valeur du paramètre 'folder' et "\" et fonction recupfilename(la valeur du paramètre file)
        FolderAndNameFile = folder + "\" + recupfilename(file)

        'Copie du fichier passé en paramètre de la fonction vers le dossier complet FolderAndNameFile
        FileCopy(file, FolderAndNameFile)

        'Passé la valeur booléenne de la fonction Copy2dir à Vrai
        Copy2Dir = True

        'aller à l'étiquette fin
        GoTo fin

erreur:
        'Etiquette erreur:

        'Valeur de la fonction Copy2Dir affectée à Faux
        Copy2Dir = False

        'Créatoin de la phrase (lerreur) avec le numéro de l'erreur, la description de l'erreur, la source de l'erreur et la demande initiale
        lerreur = DateAndTime.Now + " Err n°" + Str(Err.Number) + " Descrip : " + Err.Description + " Source : " + Err.Source + " /// demande /// --> '" + file + "' à copier vers : '" + folder + "'"

        'Affichage dans le console (dos) du message d'erreur
        Console.WriteLine(lerreur)

        '[Si] la valeur booléenne de 'ValLog' est vrai [Alors] executer la fonction WriteLog du message contenu dans lerreur
        If ValLog = True Then Call WriteLog(lerreur)

        'Aller à l'etiquette fin
        GoTo fin

erreur_taille:
        'Etiquette erreur_de_taille:

        'Valeur de la fonction Copy2Dir affectée à Faux
        Copy2Dir = False

        'Création de la phrase (lerreur) avec l'erreur interne contenant le message d'un fichier inférieur en taille à 1 octet, donc à 0.
        lerreur = DateAndTime.Now + " Err interne : la taille du fichier " + Chr(34) + file + Chr(34) + " est inférieur à 1 octet, le fichier est vide !"

        'Affichage dans la console (dos) du message (lerreur)
        Console.WriteLine(lerreur)

        '[Si] la valeur booléenne de ValLog est vrai [Alors] executer la fonction WriteLog du message contenu dans lerreur
        If ValLog = True Then Call WriteLog(lerreur)

        'Aller à l'étiquette fin
        GoTo fin
fin:
        'Ne contient aucun code, marque la fin de la fonction Copy2Dir

    End Function

    Public Function ctrl_arg(ByVal vAlarg As String, ByVal vAlRecherche1 As String, ByVal vAlRecherche2 As String) As Boolean
        'Déclaration de variables
        Dim reponseBool As Boolean 'reponseBool est un booléen (vrai ou faux)

        '[Si] la valeur de la chaîne vAlarg passé en paramètre de la fonction est identique, soit à la valeur de vAlRecherche1 ou vAlRecherche2 [Alors]
        If vAlarg = vAlRecherche1 Or vAlarg = vAlRecherche2 Then

            'reponseBool est égal à vrai
            reponseBool = True

            '[Sinon]
        Else

            'reponseBool est égal à faux
            reponseBool = False

            'fin de la condition
        End If

        'la fonction renvoi alors la valeur de reponseBool (vrai ou faux)
        Return reponseBool
    End Function

    Public Sub WriteLog(ByVal MessageLog As String)
        'Declaration des variables
        Dim FichierLog, ladateheure As String 'FichierLog et ladateheure sont des chaînes de caractères

        'la variable ladateheure est égal à la valeur de la fonction DDay() = jour + mois + année en attachés
        ladateheure = DDay()

        'la valeur de la variable FichierLog est égale au dossier executant le programme MOVE2HWMF + "\" + move2hwmf_log_" + date du jour + mois en cours + année en cours + ".txt"
        FichierLog = CurDir() + "\" + "move2hwmf_log_" + ladateheure + ".txt"

        'création si non créer / ouverture du fichier 'FichierLog" en écriture/ajout 
        FileOpen(1, FichierLog, OpenMode.Append)

        'Ecrire dnas le fichier FichierLog, le message passé en paramètre de la fonction
        PrintLine(1, MessageLog)

        'Fermeture du fichier FichierLog
        FileClose(1)

    End Sub

    Public Function DDay() As String
        'Déclaration des variables
        Dim LadateHeureDuJour, annee, mois, jour As String ' LadateHeureDuJour, annee, mois, jour sont des chaînes de caractères

        'la variable annee est égale à la valeur de l'année de (maintenant) contenu dans le module interne 'DateAndTime' du vb
        annee = DateAndTime.Year(Now)

        'la variable mois est égale à la valeur du mois actuel (maintenant) contenu dans le module interne 'DateAndTime' du vb
        mois = DateAndTime.Month(Now)

        '[Si] la longueur de caractère de la valeur du mois en cours est 1 (ex 1 = jan, 2 = fév, 3 = mars etc) [Alors] on rajoute un 0 avant la valeur du mois
        'ex : 1 devient 01; 2 devient 02; 3 devient 03 etc
        If Len(mois) = 1 Then mois = "0" + mois
        'fin de la condition

        'la variable jour est égale à la valeur du jour actuel (maintenant) contenu dans le module interne 'DateAndTime' du vb
        jour = DateAndTime.Day(Now)

        '[Si] la longueur de caractère de la valeur du jour en cours est 1, 2, 3 etc [Alors] on rajoute un 0 avant la valeur du jour
        'ex : 1 devient 01; 2 devient 02; 3 devient 03 etc
        If Len(jour) = 1 Then jour = "0" + jour

        'La variable LadateHeureDuJour est égale à la variable 'jour' + 'mois' + 'annee' (+ n'est pas une addition, c'est un ajout à la suite de)
        LadateHeureDuJour = jour + mois + annee

        'la fonction DDay retourne alors la valeur de la variable LadateHeureDuJour
        Return LadateHeureDuJour

    End Function


    Public Sub cmd_help()

        'Si aucun paramètre n'est passé lors de l'execution du programme MOVE2HWMF, exemple : "c:\move2hwmf.exe" puis valider par ENTREE,
        'Chaque ligne correspond à une partie de l'aide simplifié et s'affiche dans la console (dos)
        'le programme se termine après ça.

        'Déclaration des variables : chaque variable est une chaîne de caractère
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
        L14 = "#4 [dossier_de_destination1] ---> saisir le chemin du dossier de destination"
        L15 = "#5 [dossier_de_destination2] ---> saisir un second dossier de destination, voir un troisième où même plus en suivant la même méthode"
        L16 = ""
        L17 = "Exemple : move2hwmf -dwc=y -log=y c:\dossier\filetest.txt c:\dossier2 c:\dossier3 c:\dossier4"
        L18 = ""
        L19 = "--------------------"
        L20 = "|   FIN DE L'AIDE  |"
        L21 = "--------------------"
        L22 = ""

        'le programme affiche alors à la suite, toute les lignes de l'aide simplifié
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

        'fin de l'execution du programme

    End Sub


End Module
