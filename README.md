# MovieStore Web Api
Bu proje, .Net Web Api kullanılarak geliştirilmiş basit bir film yönetim sistemidir.
Amaç; katmanlı mimari, dependency injection, async/await ve yaygın kullanılan yardımcı kütüphaneleri
(AutoMapper, FluentValidation vb.) bilinçli şekilde kullanarak backend geliştirme pratiği kazanmaktır.

## Kullanılan Teknolojiler
- .NET 9 Web API
- Entity Framework Core
- MSSQL
- AutoMapper
- FluentValidation
- xUnit (Unit Test)
- MediatR

## Mimari
Projede katmanlı mimari yaklaşımı kullanılmıştır.

- Core: Domain modelleri ve temel abstractions
- Application: Business logic, command/query yapıları
- Infrastructure: Database ve dış bağımlılıklar
- API: Controller ve HTTP endpoint’leri

### AutoMapper
DTO ve Entity dönüşümlerini manuel yapmak yerine AutoMapper kullandım.
Bu sayede controller ve business logic katmanlarında tekrar eden mapping
kodlarını azaltmayı amaçladım.

Avantajları:
- Daha okunabilir kod
- Tekrarlayan dönüşüm kodlarının önüne geçilmesi

Dezavantajı:
- Küçük projelerde gereksiz soyutlama oluşturabilir

### FluentValidation
Model doğrulamalarını controller içinde yapmak yerine FluentValidation
kullanarak ayrı validator sınıflarında tanımladım.

Bu yaklaşım sayesinde:
- Controller'lar daha sade kaldı
- Validation kuralları tek bir yerde toplandı

### Dependency Injection
Bağımlılıklar doğrudan new'lenmek yerine constructor üzerinden enjekte edildi.
Bu sayede:

- Loose coupling sağlandı
- Unit test yazımı kolaylaştı
- Kodun sürdürülebilirliği arttı

### Async / Await
Veritabanı işlemlerinde async/await yapısı kullanıldı.
Bu sayede I/O-bound işlemlerde thread bloklanmasının önüne geçilmesi hedeflendi.

### Testler
Bu proje için unit testler xUnit ve FluentAssertions kullanılarak yazılmıştır.  
Test kapsamı şunları içerir:

- Command ve Query handler testleri
- FluentValidation kuralları için testler
- Başarılı ve başarısız senaryoların kontrolü
- In-memory EF Core kullanılarak bağımsız testler

## Amaç ve Kullanım
Bu proje backend geliştirme pratiği ve Clean Architecture örneği olarak hazırlanmıştır.  
Testler, uygulamanın güvenilirliğini ve beklenen davranışını doğrulamak için yazılmıştır.