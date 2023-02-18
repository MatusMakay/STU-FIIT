#define _CRT_SECURE_NO_WARNINGS
#define LEN 50
//REZERVUJ IZBU JE ZLE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
//
//DEFINICA STRUKTOV
typedef struct osoba 
{
	char meno[LEN + 1];
	char adresa[LEN*2 + 1];
	int rez_zac;
	int rez_kon;
	struct osoba* dalsia;
} OSOBA;

typedef struct izba 
{
	int cislo;
	int pocet_lozok;
	float cena;
	struct osoba* osoba;
	struct izba* dalsia_izba;
} IZBA;
//END
//

//
// DEFINICIA VSETKYCH FUNKCII
void vytvorZoznam(IZBA** p_p_zac);
void vypisZoznam(IZBA *p_zac);
void rezervujIzbu(IZBA **p_p_zacIzieb);
void aktualizujLozk(IZBA** p_p_zacIzieb);
void najdiInt(IZBA* p_zac);
void zrusRezervaciu(IZBA** p_p_zacIzieb);

FILE* open_file(char file_name[32], char mode[5]);
float check_string(char string[]);
OSOBA* vytvor_osobu(OSOBA make_Osoba);
IZBA* vytvor_izbu(IZBA make_Izba);
void link(IZBA *p_izba, OSOBA pole[], int limit_pole);
void fillPole(OSOBA pole[], OSOBA osoba, int posun);
int porovnaj(int num1, int num2);
void sort_linkedList(IZBA* p_p_zacOsob);
// END
//

//
//Pomocne Funkcie
FILE* open_file(char file_name[32], char mode[5])
{
	FILE* file;
	if ((file = fopen(file_name, mode)) == NULL)
	{
		printf("Zaznamy neboli nacitane\n");
		exit(1);
	}
	return file;
}
float check_string(char string[]) { //Najde v stringu cislo a vrati ho
	float num;
	int ok;
	ok = sscanf(string, "%f", &num);
	if (ok == 1)
		return num;
	else {
		printf("Subor nie je korektne nastaveny\n");
		exit(1); // Len pre istotu
	}
}
void link(IZBA *p_izba, OSOBA pole[], int limit_pole) {
	//vytvori Linked list osob a nastavi ho do izby

	OSOBA* p_zacOsob = NULL;
	OSOBA* pomOsoba = (OSOBA*)malloc(sizeof(OSOBA));

	pomOsoba = vytvor_osobu(pole[0]); 
	p_zacOsob = pomOsoba;
	//Vytvorim linked list osob
	if (limit_pole > 1) // Iba ak je viac osob ako jedna ma zmysel
	{
		int lim;
		for (lim = 1; lim < limit_pole; lim++) {
			pomOsoba->dalsia = vytvor_osobu(pole[lim]);
			pomOsoba = pomOsoba->dalsia;
		}
	}
	pomOsoba->dalsia = NULL; //ukonèim linked list
	p_izba->osoba = p_zacOsob;
} 
void fillPole(OSOBA pole[], OSOBA osoba, int posun) {
	//sluzi na uchovanie pre funkciu
	pole[posun] = osoba;
}
OSOBA* vytvor_osobu(OSOBA make_Osoba) {
	OSOBA* pom = (OSOBA*)malloc(sizeof(OSOBA));
	strcpy(pom->meno, make_Osoba.meno);
	strcpy(pom->adresa, make_Osoba.adresa);
	pom->rez_zac = make_Osoba.rez_zac;
	pom->rez_kon = make_Osoba.rez_kon;
	return pom;
}
IZBA* vytvor_izbu(IZBA make_Izba) {
	IZBA* pom = (IZBA*)malloc(sizeof(IZBA));
	pom->cislo = make_Izba.cislo;
	pom->pocet_lozok = make_Izba.pocet_lozok;
	pom->cena = make_Izba.cena;
	//Mozno pridám aj linknutie persony ešte neviu pom->osoba = zac_osoba;
	return pom;
}
int porovnaj(int num1, int num2) {
	return (num1 >= num2 ? 1 : 0);
}
void sort_linkedList(IZBA *p_zac){
	IZBA* pomocna = NULL, * tmp;
	int swap = 1;
	while (swap == 1)
	{
		swap = 0;
		for (tmp = p_zac; tmp != NULL; tmp = tmp->dalsia_izba)
		{
			if ((tmp->dalsia_izba != NULL) && (tmp->cislo > tmp->dalsia_izba->cislo))
			{
				swap = 1;
				if (pomocna == NULL)
				{
					pomocna = vytvor_izbu((*tmp));
					pomocna->osoba = tmp->osoba;
				}
				else // Nemalo by sa stat nikdy 
					exit(2);

				//Len vymenim hodnoty zo smernikmi nerobim niè
				tmp->cislo = tmp->dalsia_izba->cislo;
				tmp->pocet_lozok = tmp->dalsia_izba->pocet_lozok;
				tmp->cena = tmp->dalsia_izba->cena;
				tmp->osoba = tmp->dalsia_izba->osoba;
				tmp->dalsia_izba->cislo = pomocna->cislo;
				tmp->dalsia_izba->cena = pomocna->cena;
				tmp->dalsia_izba->pocet_lozok = pomocna->pocet_lozok;
				tmp->dalsia_izba->osoba = pomocna->osoba;

				free(pomocna);
				pomocna = NULL;
				//musim uvolnit pomocna
			}
		}
	}

}
//End PmF
//

//
//Projektove Funkcie
void vytvorZoznam(IZBA** p_p_zac) {
	FILE* sub = open_file("hotel.txt", "r");
	char string[LEN * 2];
	int posun_zapis = 0, posun_pole = 0, pocet_zaznamov = 0;
	int	zapis_izba = 0, zapis_osoby = 0, string_len;
	void* check;
	IZBA* izba_tmp = NULL, izba_ZapisHodnoty;
	OSOBA osoba_zapisHodnoty, pole_Osob[LEN/5+1];

	do
	{
		check = fgets(string, LEN * 2, sub);
		if (check == NULL)
			break;
		if (string[0] == '-') //Nastavim zapisovanie do izba
		{
			zapis_izba = 1;  
			zapis_osoby = 0;

			if (izba_tmp != NULL) // ochrana pred prvymi "---" 
				link(izba_tmp, pole_Osob, posun_pole); //Na izbu nalinkujem osoby

			posun_pole = 0;
			continue;
		}
		if (string[0] == '#') // Nastavim zapisovanie do osoby
		{
			zapis_izba = 0;
			zapis_osoby = 1;
			continue;
		}
		else
		{
			//ZAPIS IZBA
			if (zapis_izba == 1)
			{
				switch (posun_zapis) 
				{
				case 0:
					izba_ZapisHodnoty.cislo = (int)check_string(string);
					break;
				case 1:
					izba_ZapisHodnoty.pocet_lozok = (int)check_string(string);
					break;
				case 2:
					izba_ZapisHodnoty.cena = check_string(string);
					break;
				}
				posun_zapis++;

				//Vytvaranie linked listu
				if (posun_zapis == 3) 
				{
					if (izba_tmp == NULL) //nastavenie na zaciatok
					{
						izba_tmp = vytvor_izbu(izba_ZapisHodnoty);
						(*p_p_zac) = izba_tmp;
					}
					else //pridavanie na dalsie pozicie
					{
						izba_tmp->dalsia_izba = vytvor_izbu(izba_ZapisHodnoty);
						izba_tmp = izba_tmp->dalsia_izba;
					}
					posun_zapis = 0;
					pocet_zaznamov++; //pocitam zaznamy

				}

			}
			//ZAPIS OSOBA
			if (zapis_osoby == 1)
			{

				switch (posun_zapis) 
				{
				case 0:
					string_len = strlen(string);
					strcpy(osoba_zapisHodnoty.meno, string);
					osoba_zapisHodnoty.meno[--string_len] = '\0';
					break;
				case 1:
					string_len = strlen(string);
					strcpy(osoba_zapisHodnoty.adresa, string);
					osoba_zapisHodnoty.adresa[--string_len] = '\0';
					break;
				case 2:
					osoba_zapisHodnoty.rez_zac = (int)check_string(string);
					break;
				case 3:
					osoba_zapisHodnoty.rez_kon = (int)check_string(string);
					break;
				}
				posun_zapis++;

				if (posun_zapis == 4)
				{
					posun_zapis = 0;
					fillPole(pole_Osob, osoba_zapisHodnoty, posun_pole++); //všetky osoby najskor pridám do pola štruktur
				}
			}
		}

	} while (1);

	izba_tmp->dalsia_izba = NULL; //ukoncim linked list
	link(izba_tmp, pole_Osob, posun_pole);

	printf("Bolo nacitanych %d zaznamov\n", pocet_zaznamov);
	fclose(sub);
}

void vypisZoznam(IZBA* p_zac) 
{
	sort_linkedList(p_zac);
	IZBA* pom;
	//Vypisem cely zoradeny zoznam
	for (pom = p_zac; pom != NULL; pom = pom->dalsia_izba) 
	{
			printf("Izba cislo: %d\n", pom->cislo);
			printf("Pocet lozok: %d\n", pom->pocet_lozok);
			printf("Cena: %.2f\n", pom->cena);
			printf("Zoznam hosti:\n");

			OSOBA* p_zacOsob = pom->osoba;
			while (p_zacOsob != NULL) {
				printf("Meno: %s\n", p_zacOsob->meno);		
				printf("Adresa: %s\n", p_zacOsob->adresa);		
				printf("Zaciatok rezervacie: %d\n", p_zacOsob->rez_zac);		
				printf("Koniec rezervacie: %d\n", p_zacOsob->rez_kon);
				p_zacOsob = p_zacOsob->dalsia;
				if(p_zacOsob != NULL)
					printf("##################################\n");
			}
			if (pom->dalsia_izba != NULL)
				printf("----------------------------------\n"
					"----------------------------------\n");
	}
}

void rezervujIzbu(IZBA** p_p_zacIzieb) {
	FILE* sub = open_file("hotel.txt", "r+");
	char arr[LEN * 2] = { 0 }, check_arr[LEN * 2] = {0};
	int find = 0, citaj = 0;
	void* check1;
	arr[0] = getchar();
	gets(arr);
	//Kontrolujem èi už èislo izby nie je v subore -> ak by som neurobil nerešpektoval by som zadanie = "èíslo izby: (celé èíslo z intervalu <0, 10000>), predpokladajte, že ide o jedineèný identifikátor(t.j.neexistujú dve rôzne izby s rovnakým èíslom)" ak už existuje izba s cislom vypišem chybovu spravu 
	int numIzby = (int)check_string(arr);
	do
	{
		check1 = fgets(arr, LEN, sub);
		if (check1 == NULL || find == 1)
			break;
		if (arr[0] == '-')
		{
			citaj++;
			continue;
		}
		if (citaj)
		{
			find = numIzby == check_string(arr) ? 1 : 0;
			citaj--;
		}
	} while (1);

	if (find == 0)
	{
		rewind(sub);
		void * check2;
		int zapisOddelovac = 1;
		//Kotrolujem èi je na konci suboru "---" ak by bolo dvakrát nepracovalo by korektne
		do
		{
			check1 = fgets(arr, LEN * 2, sub);
			check2 = fgets(check_arr, LEN * 2, sub);
			if (check1 == NULL || check2 == NULL)
				break;
		} while (1);

		if (arr[0] == '-' || check_arr[0] == '-') 
			zapisOddelovac = 0;

		if (zapisOddelovac)
			fprintf(sub, "---\n");

		fclose(sub);

		sub = open_file("hotel.txt", "a+");

		IZBA* p_nova = (IZBA*)malloc(sizeof(IZBA));
		OSOBA* p_zacOsob = NULL, tmp, * pomocna = NULL;

		//Zapisujem aj do izby p_nova aj do suboru
		fprintf(sub, "%d\n", numIzby);
		p_nova->cislo = numIzby;
		gets(arr);
		fprintf(sub, "%s\n", arr);
		p_nova->pocet_lozok = (int)check_string(arr);
		gets(arr);
		fprintf(sub, "%s\n#\n", arr);
		p_nova->cena = check_string(arr);
		int pocetOsob;
		scanf("%d", &pocetOsob);
		arr[0] = getchar();

		//Vytváram linked list osob a rovno zapisujem aj do suboru
		while (pocetOsob-- != 0)
		{
			gets(arr);
			strcpy(tmp.meno, arr);
			fprintf(sub, "%s\n", arr);

			gets(arr);
			strcpy(tmp.adresa, arr);
			fprintf(sub, "%s\n", arr);

			gets(arr);
			tmp.rez_zac = (int)check_string(arr);
			fprintf(sub, "%s\n", arr);

			gets(arr);
			tmp.rez_kon = (int)check_string(arr);
			fprintf(sub, "%s\n", arr);

			if (p_zacOsob == NULL)
			{
				//nastavim zaciatok
				pomocna = vytvor_osobu(tmp);
				p_zacOsob = pomocna;
				if (pocetOsob > 0) //ak je viac osob ako jedna zapisem "---" do sub
					fprintf(sub, "#\n");

				continue;
			}
			pomocna->dalsia = vytvor_osobu(tmp);
			pomocna = pomocna->dalsia;
			if (pocetOsob > 0) //ak je viac osob ako jedna zapisem "---" do sub
				fprintf(sub, "#\n");
		}
		pomocna->dalsia = NULL; //Ukonèim linked list
		fclose(sub);
		//Nastavim zaciatok linked listu osob do p_nova 
		p_nova->osoba = p_zacOsob;
		p_nova->dalsia_izba = (*p_p_zacIzieb);
		(*p_p_zacIzieb) = p_nova;
	}
	else
		printf("Situacia bez presneho zadania\n");
}

void zrusRezervaciu(IZBA **p_p_zacIzieb) {
	int zrus_rezervaciu, find = 0;
	scanf("%d", &zrus_rezervaciu);

	if ((*p_p_zacIzieb) != NULL) //ak neexistuje linked list ruši -> vypisujem chybovu hlasku
	{
		IZBA* aktual = (*p_p_zacIzieb), * pred = (*p_p_zacIzieb);

		//pamatám si aktualnu a predchadzajucu cast linked listu -> ukazovali ste na cvièeni
		do
		{
			if (aktual->cislo == zrus_rezervaciu) //ak sa rovnaju breaknem a find nastavim na 1
			{
				find++;
				break;
			}
			pred = aktual;
			aktual = aktual->dalsia_izba;
		} while (aktual != NULL);

		if (find) //ak neexistuje izba so zadanim èislom tak isto vypisujem chybovu hlasku
		{
			//ak odstanujem prvy prvok
			if (aktual == (*p_p_zacIzieb)) {
				(*p_p_zacIzieb) = aktual->dalsia_izba;
				free(*p_p_zacIzieb);
				(*p_p_zacIzieb) = NULL;
			}
			//ak odstranujem iny ako prvy prvok
			else {
				pred->dalsia_izba = aktual->dalsia_izba;
				free(aktual);
				aktual = NULL;
			}
			FILE* sub = open_file("hotel.txt", "r");
			FILE* newSub = open_file("new.txt", "w");
			void* check;
			char riadok[LEN * 2];
			int pis = 1, find_numIzby = 0;
			do
			{
				check = fgets(riadok, LEN * 2, sub);
				if (check == NULL)
					break;
				if (pis == 0)
				{
					if (riadok[0] == 45) { //ak je pisanie zastavene a narazim na "---" zapnem zapisovanie do suboru
						pis = 1;
						continue;
					}
				}
				if (riadok[0] == 45) {
					find_numIzby = 1;
					fprintf(newSub, "%s", riadok);
					continue;
				}
				if (find_numIzby == 1) { //Ak najdeme --- viem že na dalsom riadku je cislo izby vtedy porovnávam 
					if (((int)check_string(riadok)) == zrus_rezervaciu) //ak som nasiel cislo izby ktore chcem odstranit zastavim zapisovanie
						pis = 0;
					find_numIzby = 0; //hned nastavim na nulu 
				}

				if (pis == 1)
					fprintf(newSub, "%s", riadok);

			} while (1);

			fclose(sub);
			fclose(newSub);

			sub = open_file("new.txt", "r");
			newSub = open_file("hotel.txt", "w");
			//prekopirujem subor s odstranenou izbou do hotel.txt
			do
			{
				check = fgets(riadok, LEN * 2, sub);
				if (check == NULL)
					break;
				fprintf(newSub, "%s", riadok);

			} while (1);

			fclose(sub);
			fclose(newSub);

			printf("Rezervacia s cislom %d bola zrusena.\n", zrus_rezervaciu);
			return;
		}
	}
	printf("Situacia bez presneho zadania\n");
}

void aktualizujLozk(IZBA **p_p_zacIzieb) {
	
	int findNum, aktualizuj, find = 0;
	scanf("%d", &findNum);
	scanf("%d", &aktualizuj);

	if ((*p_p_zacIzieb) != NULL) //ak neexistuje linked list nie je èo aktualizova vypisujem chybovu hlasku
	{
		IZBA* pom = (*p_p_zacIzieb);
		int pocet = 0;
		do
		{
			if (pom->cislo == findNum)
			{
				OSOBA* p_zacOsob = pom->osoba;
				while (p_zacOsob != NULL)
				{
					pocet++;
					p_zacOsob = p_zacOsob->dalsia;
					if (pocet > aktualizuj) //ak pocet osob na izbe prekroci nastavenu hranicu vypisujem chybovu hlasku -> Vychádzam z logickej úvahy: 3 osoby nemôžu by na 2 ložkovej izbe
					{
						printf("Situacia bez presneho zadania\n");
						return;
					}
				}
				pom->pocet_lozok = aktualizuj;
				find++;
				break;
			}
			pom = pom->dalsia_izba;
		} while (pom != NULL);

		if (find)  //ak sa nenájde izba ktorej chcem zmenit ložkovu kapacitu vypisujem chybovu hlasku
		{
			printf("Izba cislo %d ma lozkovu kapacitu %d\n", findNum, aktualizuj);
			return;
		}
	}
	printf("Situacia bez presneho zadania\n");
}

void najdiInt(IZBA* p_zac) {
	int datum, find_Izba = 0, check = 0;
	scanf("%d", &datum);

	if (p_zac != NULL) // ak nie je vytvoreny linked list vypisujem chybovu hlasku -> Vychádzam z úvahy: 1.Vo funkcii nemám otvára subor  2.ak je v subore osoba ktorá bude na izbe v daný dátum nebudem o tom vedie -> vysledok funkcie by nebol korektný
	{
		sort_linkedList(p_zac);


		IZBA* izba_pom = p_zac;
		do
		{
			OSOBA* p_zacOsob = izba_pom->osoba;
			
			//kontrolujem pre všetky osoby na izbe
			while (p_zacOsob != NULL)
			{
				if (porovnaj(datum, p_zacOsob->rez_zac) == 1 && porovnaj(p_zacOsob->rez_kon, datum) == 1) // volam funkciu ktorá vracia 1 ak prvý argument ktorý jej odovzdám je vaèší ako druhy
					find_Izba = 1;

				p_zacOsob = p_zacOsob->dalsia;
			}

			if (find_Izba) //ak nájdem osobu ktorá je na izbe v daný dátum vypíšem èislo izby, zvaèší hodnotu check
			{
				printf("%d\n", izba_pom->cislo);
				find_Izba = 0;
				check++; 
			}
			izba_pom = izba_pom->dalsia_izba;

		} while (izba_pom != NULL);
		if (check == 0) //ak sa nenájde žiadna
			printf("K datumu %d neevidujeme rezervaciu\n", datum);
		return;
	}
	printf("Situacia bez presneho zadania\n");
}
//End PF
//

int main(void) {
	IZBA* zac = NULL;
	char c = 'L';
	while(c!='k')
	{
		c = getchar();
		switch (c)
		{
			case 'n':

				if(zac == NULL)
					vytvorZoznam(&zac);
				else
				{
					IZBA* p_aktIzbu;
					OSOBA* p_zacOsob, * p_aktOsob = NULL;

					do
					{
						p_zacOsob = zac->osoba;

						while (p_zacOsob != NULL)
						{
							p_aktOsob = p_zacOsob;
							p_zacOsob = p_zacOsob->dalsia;
							free(p_aktOsob);
							p_aktOsob = NULL;
						}
						p_aktIzbu = zac;
						zac = zac->dalsia_izba;
						free(p_aktIzbu);
						p_aktIzbu = NULL;

					} while (zac != NULL);

					vytvorZoznam(&zac);
				}
				break;

			case 'v':
				if (zac != NULL)
					vypisZoznam(zac);
				else
					continue;
				break;

			case 'r':
				rezervujIzbu(&zac);
				break;

			case 'z':
				zrusRezervaciu(&zac);
				break;

			case 'h':
				najdiInt(zac);
				break;

			case 'a':
				aktualizujLozk(&zac);
				break;
		}
	}

	if(zac != NULL)
	{
		IZBA* p_aktIzbu;
		OSOBA* p_zacOsob, * p_aktOsob = NULL;

		do
		{
			p_zacOsob = zac->osoba;

			while (p_zacOsob != NULL) 
			{
				p_aktOsob = p_zacOsob;
				p_zacOsob = p_zacOsob->dalsia;
				free(p_aktOsob);
				p_aktOsob = NULL;
			}
			p_aktIzbu = zac;
			zac = zac->dalsia_izba;
			free(p_aktIzbu);
			p_aktIzbu = NULL;

		} while (zac != NULL);
	}
	return 0;
}
