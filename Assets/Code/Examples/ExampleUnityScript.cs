// Importataan tiedostoon mukaan paketit, joita luokka k‰ytt‰‰.
using UnityEngine;

// Jokainen luokka kannattaa kirjoittaa jonkin nimiavaruuden sis‰‰n. 
// Nimiavaruuksien ansiosta voimme antaa luokallemme saman nimen, joka
// voi olla olemassa jossain k‰ytt‰m‰ss‰mme kirjastossa. Nimiavaruudet
// erottavat n‰m‰ saman nimiset luokat toisistaan.
namespace Platformer2D
{
	/// <summary>
	/// Luokan m‰‰rittely. Luokka on kokonaisuus, joka koostuu toiminnallisuudesta
	/// (metodit) ja datasta, eli olion tilasta (j‰senmuuttujat).
	/// Unityss‰ kaikki luokat, jotka kytket‰‰n GameObjecteihin, periytyv‰t MonoBehaviour-
	/// luokasta.
	/// </summary>
	public class ExampleUnityScript : MonoBehaviour
	{
		// Olion j‰senmuuttuja. Jokaisella oliolla on tallessa oma arvonsa t‰st‰
		// muuttujasta. N‰kyvyysm‰‰reet:
		// - private: arvo voidaan lukea ja sit‰ voidaan muuttaa vain t‰st‰ luokasta
		// - public: arvo voidaan lukea ja sit‰ voidaan muuttaa mist‰ vain
		// J‰senmuuttujat kannattaa m‰‰ritt‰‰ privateksi. Niiden muokkaaminen
		// luokan ulkopuolelta tekee koodista bugiherkemp‰‰.
		// T‰m‰n j‰senmuuttujan tyyppi on int eli kokonaisluku.
		// J‰senmuuttujaa voidaan k‰ytt‰‰ niin kauan, kun olio on olemassa.
		private int frameCounter = 5;

		// T‰h‰n j‰senmuuttujaan tallennetaan viittaus toiseen olioon, josta
		// t‰st‰ luokasta instantioitu olio on riippuvainen. 
		private SpriteRenderer spriteRenderer;

		// Olion j‰senmuuttuja, jonka arvo voidaan asettaa Unity editorista.
		// [SerializeField] attribuutin kirjoittaminen muuttujan eteen tekee siit‰
		// muokattavan Unity:ss‰. Lis‰ksi t‰m‰ mahdollistaa sen, ett‰ muuttujan
		// arvon voi tallentaa sceneen tai prefabiin. T‰m‰n ansiosta samassa
		// muuttujassa voi olla eri arvo eri sceneiss‰.
		[SerializeField] private int numero1 = 10;

		/// <summary>
		/// Unityn automaattisesti suorittama metodi.
		/// Suoritetaan heti, kun olio ladataan tietokoneen muistiin.
		/// K‰ytet‰‰n mm. luokan riippuvuuksien hakemiseen ja 
		/// sis‰isen toiminnallisuuden alustamiseen. Korvaa rakentajan, jota
		/// ei voida k‰ytt‰‰ MonoBehavioureiden kanssa.
		/// </summary>
		private void Awake()
		{
			// Esimerkki j‰senmuuttujan arvon alustamisesta:
			frameCounter = 0;

			// Haetaan viittaus samassa GameObjectissa olevaan
			// SpriteRenderer tyyppiseen olioon.
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// Unityn automaattisesti suorittama metodi.
		/// Suoritetaan, kun olio aktivoidaan 1. kerran, juuri
		/// ennen 1. Update-metodikutsua.
		/// Voidaan k‰ytt‰‰ mm. ulkoisten riippuvuuksien alustamiseen.
		/// </summary>
		private void Start()
		{
			// T‰m‰ esimerkki n‰ytt‰‰, miten muuttujan arvoa voidaan muuttaa
			// Unityn editorissa.
			Debug.Log($"Muuttujan numero1 arvo on {numero1}");
		}

		/// <summary>
		/// Unityn automaattisesti suorittama metodi.
		/// Suoritetaan kerran jokaisen framen aikana. Voidaan k‰ytt‰‰
		/// pitk‰aikaisen toiminnallisuuden (esim. hahmon liikuttamisen)
		/// toteuttamiseen.
		/// </summary>
		private void Update()
		{
			// FrameCounter-muuttujan arvo s‰ilyy Update-kutsujen yli, koska
			// se on m‰‰ritetty luokan j‰senmuuttujana.
			frameCounter++;

			// Paikallinen muuttuja. T‰m‰ muuttuja on luotu metodin sis‰ll‰
			// ja se lakkaa olemasta, kun metodin suoritus lakkaa.
			string message = $"Frame {frameCounter}";

			// Tulostaa jokaisella framella tiedon siit‰, monesko frame suoritettiin.
			// Tulos n‰kyy Unity:n consolessa.
			Debug.Log(message);
		}
	}
}
