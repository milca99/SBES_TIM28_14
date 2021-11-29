# SBES_TIM28_14
Potrebno je realizovati sistem za slanje žalbi gradskoj vlasti.
Postoje 2 korisničke grupe, koje imaju različita prava unutar sistema:
● Nadzor
● Korisnik
Korisnici se pokušavaju autentifikovati na sistem korišćenjem sertifikata.
Potrebno je izvršiti custom validaciju na strani servera, tako što će se proveravati da li taj
korisnik, ili izdavalac njegovog sertifikata se nalaze u fajlu banned_certs.xml na strani
servera. Ukoliko se nalaze, validator ne dozvoljava korisniku dalje korišćenje sistema. U
suprotnom, korisnik je uspešno pristupio sistemu i može da šalje žalbe (u vidu tekstualnih
poruka).
Žalbe može slati samo član grupe Korisnik, a poruke koje se šalju servisu moraju biti
digitalno potpisane.
Sistem poseduje fajl sa nedozvoljenim rečima. Nadzor može zatražiti od sistema da izlista
sve žalbe koje u sebi sadrže makar jednu nedozvoljenu reč. Nadzor potom za svaku od njih
odlučuje da li će da kazni korisnika koji je napisao tu žalbu dodavanjem korisnikovih detalja
u banned_certs.xml (kako više nikada ne bi mogao da pristupi sistemu), ili da mu oprosti.
Kažnjavanje korisnika od strane nadzora, kao i njegovo opraštanje, logovati pomoću
Windows Event Log-a.
Napomena: U okviru sertifikata u SubjectName treba upisati za CN korisnicko ime, a za OU
grupu kojoj korisnik pripada.
