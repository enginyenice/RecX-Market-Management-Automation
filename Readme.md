** [Tam Rapor](https://github.com/nginY26/RecX-Market-Otomasyonu/blob/master/Rapor.pdf "Tam Rapor") **

## KOCAELİ ÜNİVERSİTESİ
###  KOCAELİ MESLEK YÜKSEKOKULU
###  BİLGİSAYAR TEKLONOJİLERİ BÖLÜMÜ
####BİLGİSAYAR PROGRAMCILIĞI PROGRAMI
#### MARKET OTOMASYONU
##### ENGİN YENİCE
##### KOCAELİ 2017
ÖNSÖZ VE TEŞEKKÜR
Yardımlarını eksik etmeyen değerli hocalarıma teşekkür ederim. Ayrıca hayatım
boyunca beni destekleyen aileme ve hayallerimi gerçekleştirmemde katkısı olan
herkese sonsuz minnet duygularımı sunarım.

Aralık - 2017 Engin YENİCE

ÖZET
Süpermarket, marketlerde ve bakkallarda iş kolaylığı sağlamak amacıyla
geliştirilmiştir. Yapılan projede C# dili ve ACCESS veri tabanı kullanılmıştır. Market
otomasyonunda birçok alanda geliştirilmeler yapılmıştır ve bu geliştirmeler:

Güvenli giriş
Müşteri kontrolü
Satış kontrolü
Ürün (ekleme, düzenleme ve silme) işlemleri
Ürün işlemleri yaparak ürünlerin daha düzenli ve kontrol edilebilir hale getirilmesi
planlanmıştır. Satış kontrolü ile yapılan kazançların kaydı tutulması planlanmıştır.
Müşteri kontrolü eklenerek firmaya gelen müşterilerin bilgilerinin saklandığı bir veri
tablosu geliştirilerek firmanın müşteriler ile iletişim sağlanması hedeflenmiştir.
Güvenli giriş sistemleri ile sisteme güvenilir ve yetkiye göre işlemlerin yapılabileceği
bir basit arayüz tasarlanarak yapılacak işlerin daha hızlı gerçekleştirilmesi için
çalışmalar yapılmıştır.

Anahtar Kelimeler: ACCESS, C# , Kolay ara yüz, Market Otomasyonu, RecX

iii
ABSTRACT
It was developed to provide business convenience to supermarkets, markets and
grocery stores. C# language and ACCESS database were used in the project. Many
field improvements have been made in the automation of the market and these
developments are:

Secure login
Customer control
Sales check
Product (add, edit and delete) operations
It is planned to make the products more regular and controllable by carrying out
product operations. It is planned to record the earnings made by sales control. By
adding a customer control, a database storing the information of the customers coming
to the company was developed and it was aimed to communicate with the customer of
the company. Safe login systems have been designed to design a simple interface that
can be trusted and trusted by the system to perform tasks faster.

Key words: ACCESS, C #, Easy interface, Market automation, RecX

GİRİŞ
Proje marketlerdeki iş kolaylığının ve kontrolünün sağlanmasıdır. Projenin
konusu “market otomasyonu programının geliştirilmesi"dir. Projede kullanılan

programlama dili olarak C# kullanılmıştır. Veri tabanı olarak ACCESS tercih

edilmiştir.

İş kolaylığının sağlanması için yapılan çalışmalar:

Stok kontrolü

Müşteri kontrolü

Kasa kontrolü

İş kontrolünün sağlanması için yapılan çalışmalar:

Stok raporlarının oluşturulması

Müşteri raporlarının oluşturulması

Kasa raporlarının oluşturulması

Fiş yazdırılarak anlık satış raporunun oluşturulması

Stok kontrolü alanında yapılan geliştirmelerde ürünlerin kategorilere ayrılması
sonucunda daha kontrollü bir yapı oluşturulmuştur. Ürünleri sisteme ekleme,

düzenleme ve silme işlemleri gerçekleştirilebilir. Anlık olarak ürün listesinin raporları

alınabilir.

Müşteri kontrolü alanında yapılan geliştirmelerde, müşterilerin sistemde
kayıtları tutularak müşteriler hakkında bilgiler alınabilir. Anlık olarak tüm müşterilerin

listelendiği bir raporlama sistemi oluşturulabilir.

Kasa kontrolü alanında yapılan geliştirmelerde satış işlemi gerçekleştirildikten
sonra veriler kasaya kayıt edilmektedir. Bu kayıt işlemlerinin sonucunda kasadan

günlük ve aylık satış verilerine ulaşılabilir. Tüm satış verilerinin bulunduğu bir rapor

alınabilir.

Bu geliştirmeler sonrasında proje tamamlandı ve adı “RecX Market
Otomasyonu” olarak belirlendi.

GENEL BİLGİLER
C# Nedir?
1950’li yıllardan başlayıp günümüze kadar uzanan yazılım sektöründe her
geçen gün yeni şeyler duymak mümkün. Çünkü insanlık tarih boyunca işlerini

kolaylaştıracak ürünler üretmeye meyilli olmuştur. Programlama ise 20. ve 21.

yüzyılda insanların hayatını kolaylaştıran ürünler üretmek için kullanılan bir araç

olmuştur.

Günümüze kadar pek çok programlama dilleri geliştirilmiştir. Bunlar
kullanılacak platformlara göre ya da dil yapısına göre farklı alanlarda kullanılır. Tüm

dillerden arasında özellikle nesnel programlama alanında iki programlama dili insanlık

için oldukça önemlidir. Bu dillerin ilki ortak platform olarak çalıştırılabilen Java

ikincisi ise.net kütüphanesi ile bütünleşmiş edilerek tüm dillerle ortak platformda

programlanabilir ve kolay kodlama yapısı ile C# (CSharp) programlama dilidir.

Microsoft Access Nedir?
Access, İlişkisel Veri Tabanı Yönetim Sistemi ile çalışan bir veri tabanı
oluşturma programıdır. İlişkisel Veri Tabanı Yönetim Sistemi sisteminde bir veri

tabanı dosyasında birden fazla tablo oluşturulabilir ve bu tablolar arasında birbirleriyle

ilişki kurulabilir. Kurulan ilişkiler sayesinde farklı tablolardaki veriler sanki aynı

tablodaymış gibi kullanılabilir.

Microsoft Access bir İlişkisel Veri Tabanı Yönetim Sistemi uygulamasıdır. Bir
veri tabanını oluşturmak ve kullanmak Access ile diğer veri tabanı uygulamalarına

göre çok daha kolaydır. Bunun nedeni Access’in, Windows ortamının Grafiksel

Kullanıcı Arabiriminin sağladığı avantajların tümünden yararlanma imkânı

vermesidir. Grafiksel Kullanıcı Arabirimi, karmaşık komut dizilerini öğrenmeyi

gerektirmeden, ekran üzerindeki nesneler ve simgeler yardımıyla, fare desteğinden de

yararlanarak kullanıcının çalışmasına olanak verir.

KAYNAKLAR
ACCESS&SQL: http://www.access-sql.com/
Ahmet Can Sever: http://www.ahmetcansever.com/programlama/c-access-veri-
tabani-baglantisi-select-insert-update-delete-ornek-uygulama/

Hikmet Okutmuş: http://www.hikmetokumus.com/makale/11/datagridview%27-da-
bulunan-satirlari-yazdirmak

Microsoft Social : https://social.msdn.microsoft.com

ÖZGEÇMİŞ
1998 yılında Eskişehir’de doğdu. İlkokulu ve ortaokulu Eskişehir’in Mahmudiye

ilçesinde Necatibey İlköğretim Okulun’da tamamladı. Liseyi Eskişehir’de Toki Şehir

İsmail Tetik Anadolu Lisesinde tamamladı. 2016 yılında Kocaeli Üniversitesi

Bilgisayar Programcılığı bölümüne başladı ve okumaya devam etmektedir.

This is a offline tool, your data stays locally and is not send to any server!
Feedback & Bug Reports