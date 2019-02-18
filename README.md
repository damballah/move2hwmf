## MOVE2HWMF (Move to Hello World Mother Fucker)

**move2hwmf** est application en ligne de commande Windows qui revisite le programme "move" (deplacement de fichiers) de manière avancée avec un système de log et de copie multi-dossiers.

**Son utilisation comporte plusieurs possibilités :** 

- Copier un fichier dans plusieurs dossiers en utilisant une seule commande.
- Possibilité de créer un fichier de log.
- Supprimer où non de manière automatique le fichier source une fois la copie terminée.
#### Exemple d'utilisation :

    > move2hwmf.exe -dwc=y -log=y c:\fichier.txt d:\dossier1 d:\dossier2

Dans cet exemple, la commande va copier le ***fichier.txt*** vers les dossiers  
***d:\dossier1*** et ***d:\dossier2***. 
Le programme supprimera le fichier source une fois l'opération terminée (**-dwc=y**) et il écrira dans le fichier de log (**-log=y**). 

Vous pouvez ajouter autant de dossiers de destination que vous souhaitez en paramètres.
#### Précisions sur les paramètres:
La commande **move2hwmf.exe** possède 2 arguments qu'il faut impérativement passer en premier et second avant d'indiquer le fichier source à copier ainsi que le/les dossiers de destinations.

 **Premier argument:**
 -  `[-dwc=y] où [-dwc=n]` *Effacement du fichier source une fois l'opération terminée.*
       *(y=oui ; n=non)*

**Second argument:**
 - `[-log=y] où [-log=n]` *Ecriture dans un fichier de log de l'activité du programme*
 *(y=oui ; n=non)*
 
**Troisième argument:**
 - Le chemin complet du fichier à copier jusqu'au fichier source lui même
Exemple : `c:\dossiersource\fichier.txt`

**Quatrième argument:**

 - Le chemin du dossier de destination
 Exemple : `d:\dossierdedestination1`
 
 **Arguments supplémentaires :** 
 *Ajout d'autres dossiers de destinations*
 
 - Le chemin d'un deuxième dossier de destination
 Exemple :  `d:\dossierdedestination2`
 
 - Le chemin d'un troisième dossier de destination
 Exemple :  `d:\dossierdedestination3`
 > *Ainsi de suite*


**Le fichier de log:**
Ce fichier est créer dans le dossier de l'application et se nomme de cette façon : 
***"move2hwmf_log_" + JourMoisAnnée + ".txt"***

Exemple pour un fichier de log crée le 18 Février 2019:

    move2hwmf_log_180220019.txt

**La gestion d'erreurs:**
- ***Erreurs rencontrés de manière native:*** 
accès fichier(s), accès dossier(s)
- ***Erreurs gérés par le programme:*** 
- Un fichier d'une taille inférieur à 1 octet
- Une erreur de frappe où d'ordre dans les 2 premiers arguments
- Nombre d'arguments par défaut (4)


> Toutes ces erreurs sont inscrites dans le fichier de log et/ou
> afficher dans la console.

**Pour afficher l'aide en mode console:**
Tapez simplement `move2hwmf` à l'invite de commande puis ENTREE.
