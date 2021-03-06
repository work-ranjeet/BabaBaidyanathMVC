--------------- Services ------------------------
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (1,NULL,1,'General Puja/Worship', NULL, N'सामान्य पूजा ',NULL,NULL, 0, 1, 1)
						
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			values (2,NULL,1,'Special Worship', NULL, N'विशेष पूजा',NULL,NULL, 0, 2, 1)
			
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			values (3,NULL,1,'Other Activities', NULL, N'अन्य क्रियाकलाप / गतिविधि',NULL,NULL, 0, 3, 1)
			
-- no child og general puja
--child of Special Worship			
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (200,2,1,'Panchopchar Pujan of Baba Baidyanath', NULL, N'पंचोपचार पूजा',NULL,NULL, 0, 1, 1)
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (201,2,1,'Dasopchar Pujan of Baba Baidyanath', NULL, N'दशोपचार पूजा',NULL,NULL, 0, 2, 1)
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (202,2,1,'Sodsopchar Pujan of Baba Baidyanath', NULL, N'षोडशोपचार पूजा',NULL,NULL, 0, 3, 1)
			 
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (203,2,1,'Battisopchar Puja of Baba Baidyanath', NULL, N'बत्तिसोपचार पूजा',NULL,NULL, 0, 4, 1)
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (204,2,1,'Chosathopchar Pujan of Baba Baidyanath', NULL, N'चोसठोपचार पूजा',NULL,NULL, 0, 5, 1)
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (205,2,1,'Maha Mrityunjay Jap', NULL, N'महा मृत्युंजय जप ',NULL,NULL, 0, 6, 1)
			 
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (206,2,1,'Rudrabhishek of Baba Baidyanath', NULL, N'रुद्राभिषेक',NULL,NULL, 0, 7, 1)
			 
--Child of other activities
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (300,3,1,'Kunwari Pujan (Offering Food and useful thingsTo small And unmarried Girls)', NULL, N'कुँवारी भोजन',NULL,NULL, 0, 1, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (301,3,1,'Brahman Bhojan (Offering Food To Pundits / Brahmins / Vaidiks)', NULL, N'ब्राह्मण भोजन',NULL,NULL, 0, 2, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (302,3,1,'Daridra Narayan Bhojan (Offering Food To Poor and Needy Peoples)', NULL, N'दरिद्र नारायण भोजन',NULL,NULL, 0, 3, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (303,3,1,'Vastra(Cloth) Danam (Offering cloths to Brahmins/ Kunwaries / Poor Peoples / Vaidiks)', NULL, N'वस्त्र दान',NULL,NULL, 0, 4, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (304,3,1,'Gaushala', NULL, N'गौशाला',NULL,NULL, 0, 5, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (305,3,1,'Gupt Danam', NULL, N'गुप्त दान',NULL,NULL, 0, 6, 1)
			 
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			 values (306,3,1,'Others', NULL, N'अन्य',NULL,NULL, 0, 7, 1)
			 
			 
			 ---child for 205 - ma mritunjay jap
			 insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			values (20500,205,1,'101 - Onward Jap', NULL, N'101 से असंख्य जप',NULL,NULL, 0, 1, 1)
			 
			insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			values (20501,205,1,'44000 Jap', NULL, N'44000 जप',NULL,NULL, 0, 2, 1)
			 
			insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
			values (20502,205,1,'125000 Jap', NULL, N'125000 जप',NULL,NULL, 0, 3, 1)
			
			-- child 0f 206 - rudrshbhisek	
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (20600,206,1,'Sad-anag Rudrabhisek', NULL, N'षड-अंग रुद्राभिषेक',NULL,NULL, 0, 1, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (20601,206,1,'Namak-Chamak Rudrabhisek', NULL, N'नमक चमक रुद्राभिषेक',NULL,NULL, 0, 2, 1)


-- 20600		Sad-anag Rudrabhisek	
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060000,20600,1,'By One Vaidik', NULL, N'एक वैदिक द्वारा',NULL,NULL, 0, 1, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060001,20600,1,'By Two Vaidiks', NULL, N'दो वैदिकों द्वारा',NULL,NULL, 0, 2, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060002,20600,1,'By Three Vaidiks', NULL, N'तीन वैदिकों द्वारा',NULL,NULL, 0, 3, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060003,20600,1,'By Five Vaidiks', NULL, N'पांच वैदिकों द्वारा',NULL,NULL, 0, 4, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060004,20600,1,'By Seven Vaidiks', NULL, N'सात वैदिकों द्वारा',NULL,NULL, 0, 5, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060005,20601,1,'By Eleven Vaidiks', NULL, N'इग्यारह वैदिकों द्वारा',NULL,NULL, 0, 6, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060006,20600,1,'Ati Rudrabhisek : By Eleven Vaidiks For 11 Days Continously', NULL, N' "अतिरुद्राभिषेक" : इग्यारह वैदिकों द्वारा (इग्यारह दिन तक लगातार)',NULL,NULL, 0, 7, 1)

-- 20601		Sad-anag Rudrabhisek	
insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060100,20601,1,'By One Vaidik', NULL, N'एक वैदिक द्वारा',NULL,NULL, 0, 1, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060101,20601,1,'By Two Vaidiks', NULL, N'दो वैदिकों द्वारा',NULL,NULL, 0, 2, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060102,20601,1,'By Three Vaidiks', NULL, N'तीन वैदिकों द्वारा',NULL,NULL, 0, 3, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060103,20601,1,'By Five Vaidiks', NULL, N'पांच वैदिकों द्वारा',NULL,NULL, 0, 4, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060104,20601,1,'By Seven Vaidiks', NULL, N'सात वैदिकों द्वारा',NULL,NULL, 0, 5, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060105,20601,1,'By Eleven Vaidiks', NULL, N'इग्यारह वैदिकों द्वारा',NULL,NULL, 0, 6, 1)

insert into services(ServiceID, ServiceParentID, ServiceTypeID, ServiceTitle, ServiceInformation, ServiceTitleInd, ServiceInformationInd, ServiceIcon,ServiceLikeCount, ServiceOrder, IsActive)
values (2060106,20601,1,'Ati Rudrabhisek: By Eleven Vaidiks For 11 Days Continously', NULL, N' "अतिरुद्राभिषेक" : इग्यारह वैदिकों द्वारा (इग्यारह दिन तक लगातार)',NULL,NULL, 0, 7, 1)
