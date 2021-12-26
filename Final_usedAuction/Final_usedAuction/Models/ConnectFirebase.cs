using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Firebase.Database;
using Firebase.Database.Query;

namespace Final_usedAuction.Models
{
    class ConnectFirebase
    {
        FirebaseClient firebase = new FirebaseClient("https://fianl-pjt-default-rtdb.firebaseio.com/");
        //-----------------------------------------계정관련 함수--------------------------------------------------
        // 로그인 함수
        public async Task<List<Member>> L_GetAllMembers()
        {
            return (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Select(Item => new Member
                {
                    MemberID = Item.Object.MemberID,
                    MemberPW = Item.Object.MemberPW
                }).ToList();
        }
        //로그인 함수에서 조건에 맞는 ID PW 검색
        public async Task<Member> L_GetMember(string memID, string memPW)
        {
            var allMember = await L_GetAllMembers();
            await firebase
                .Child("Member")
                .OnceAsync<Member>();
            return allMember.Where(a => a.MemberID == memID && a.MemberPW == memPW).FirstOrDefault();
        }

        // ID 중복확인
        public async Task<List<Member>> ID_CK_GetAllMembers()
        {
            return (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Select(Item => new Member
                {
                    MemberID = Item.Object.MemberID
                }).ToList();
        }
        // ID 중복확인에서 조건에 맞는 ID 검색
        public async Task<Member> ID_CK_GetMember(string memID)
        {
            var allMember = await ID_CK_GetAllMembers();
            await firebase
                .Child("Member")
                .OnceAsync<Member>();
            return allMember.Where(a => a.MemberID == memID).FirstOrDefault();
        }

        // 계정명 중복확인
        public async Task<List<Member>> NNAME_CK_GetAllMembers()
        {
            return (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Select(Item => new Member
                {
                    MemberNName = Item.Object.MemberNName
                }).ToList();
        }
        // 계정명 중복확인에서 조건에 맞는 계정명 검색
        public async Task<Member> NNAME_CK_GetMember(string memNName)
        {
            var allMember = await NNAME_CK_GetAllMembers();
            await firebase
                .Child("Member")
                .OnceAsync<Member>();
            return allMember.Where(a => a.MemberNName == memNName).FirstOrDefault();
        }

        // 계정정보 탐색 
        public async Task<List<Member>> Select_GetAllMembers()
        {
            return (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Select(Item => new Member
                {
                    MemberID = Item.Object.MemberID,
                    MemberName = Item.Object.MemberName,
                    MemberAnswer = Item.Object.MemberAnswer,
                    MemberEMail = Item.Object.MemberEMail,
                    MemberKAID = Item.Object.MemberKAID,
                    MemberNName = Item.Object.MemberNName,
                    MemberPhone = Item.Object.MemberPhone,
                    MemberPW = Item.Object.MemberPW,
                    MemberSatissfied = Item.Object.MemberSatissfied,
                    MemberTrust = Item.Object.MemberTrust,
                    MemberT_count = Item.Object.MemberT_count,
                    MemberB_count = Item.Object.MemberB_count,
                    MemberS_count = Item.Object.MemberS_count

                }).ToList();
        }
        // 조건에 맞는 계정정보 확인
        public async Task<Member> Select_GetMember(string memID)
        {
            var allMember = await Select_GetAllMembers();
            await firebase
                .Child("Member")
                .OnceAsync<Member>();
            return allMember.Where(a => a.MemberID == memID).FirstOrDefault();
        }

        //계정 수정
        public async Task UpdateMember(string mem_Id, string mem_pw, string mem_nname, string mem_phone, string mem_email, string mem_kaid)
        {
            var toUpdateMember = (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            var meminfo = await Select_GetMember(mem_Id);
            meminfo.MemberPW = mem_pw;
            meminfo.MemberKAID = mem_kaid;
            meminfo.MemberPhone = mem_phone;
            meminfo.MemberNName = mem_nname;
            meminfo.MemberEMail = mem_email;
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberPW")
                 .PutAsync<string>(meminfo.MemberPW);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberKAID")
                 .PutAsync<string>(meminfo.MemberKAID);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberPhone")
                 .PutAsync<string>(meminfo.MemberPhone);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberNName")
                 .PutAsync<string>(meminfo.MemberNName);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberEMail")
                 .PutAsync<string>(meminfo.MemberEMail);


        }
        //계정 신뢰도 수정
        public async Task UpdateMember(string mem_Id,int trust, int answer, int satissfied)
        {
            var toUpdateMember = (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            var meminfo = await Select_GetMember(mem_Id);
            meminfo.MemberTrust+= trust;
            meminfo.MemberAnswer+= answer;
            meminfo.MemberSatissfied+= satissfied;
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberTrust")
                 .PutAsync<int>(meminfo.MemberTrust);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberAnswer")
                 .PutAsync<int>(meminfo.MemberAnswer);
            await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberSatissfied")
                 .PutAsync<int>(meminfo.MemberSatissfied);
        }
        //계정 거래횟수 증가
        public async Task UpdateMember(string mem_Id, int t_sw) // t_sw : 거래 증가 스위치 1 = 판매증가, 2 = 구매증가 
        {
            var toUpdateMember = (await firebase
                .Child("Member")
                .OnceAsync<Member>()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            var meminfo = await Select_GetMember(mem_Id);
            if (t_sw == 1)
            {
                meminfo.MemberS_count++;
                meminfo.MemberT_count++;
                await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberS_count")
                 .PutAsync<int>(meminfo.MemberS_count);
                await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberT_count")
                 .PutAsync<int>(meminfo.MemberT_count);
            }
            else
            {
                meminfo.MemberB_count++;
                meminfo.MemberT_count++;
                await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberB_count")
                 .PutAsync<int>(meminfo.MemberB_count);
                await firebase
                 .Child("Member")
                 .Child(toUpdateMember.Key)
                 .Child("MemberT_count")
                 .PutAsync<int>(meminfo.MemberT_count);
            }
        }
        //계정 추가
        public async Task AddMember(string mem_Id, string mem_pw, string mem_name, string mem_nname, string mem_phone, string mem_email, string mem_kaid)
        {

            await firebase
              .Child("Member")
              .PostAsync(new Member() { MemberID = mem_Id, MemberPW = mem_pw , MemberName = mem_name, MemberNName = mem_nname, MemberEMail = mem_email,
                  MemberKAID = mem_kaid, MemberPhone = mem_phone, MemberT_count = 0, MemberAnswer = 0, MemberSatissfied = 0, MemberTrust = 0, MemberB_count = 0, MemberS_count = 0});

        }

        //----------------------------------------------------------------------------------------------------------------
        //------------------------------------경매 게시글 관련-------------------------------------------------
        // 관심 게시글 추가
        public async Task AddInerItem(string mem_Id, int s_num, string s_name, string s_con, string s_img, int s_price, string s_id, string t_id, int h_bid, int s_mode)
        {

            var toAddInterItem = (await firebase
                .Child("Member")
                .OnceAsync<Member>
                ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            await firebase
                .Child("Member")
                .Child(toAddInterItem.Key)
                .Child("Inter_item")
                .PostAsync(new Inter_item()
                {
                    Sell_contents = s_con,
                    Sell_name = s_name,
                    Sell_ID = s_id,
                    Sell_img = s_img,
                    Sell_num = s_num,
                    Sell_price = s_price,
                    Topbid_ID = t_id,
                    Highest_bid = h_bid,
                    Sell_mode = s_mode
                }
                    ) ;

        }
        //관심 게시글 전체 검색
        public async Task<List<Inter_item>> Select_GetAllInterItem(string mem_Id)
        {
            var toAddinterItem = (await firebase
             .Child("Member")
             .OnceAsync<Member>
             ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            return (await firebase
                .Child("Member")
                .Child(toAddinterItem.Key)
                .Child("Inter_item")
                .OnceAsync<Inter_item>()).Select(Item => new Inter_item
                {
                    Sell_name = Item.Object.Sell_name,
                    Sell_contents = Item.Object.Sell_contents,
                    Sell_ID = Item.Object.Sell_ID,
                    Sell_img = Item.Object.Sell_img,
                    Sell_num = Item.Object.Sell_num,
                    Sell_price = Item.Object.Sell_price,
                    Topbid_ID = Item.Object.Topbid_ID,
                }).ToList();
        }
        //관심 게시글 검색
        public async Task<Inter_item> Select_GetInterItem(string mem_Id , int s_num)
        {
            var toAddInter = (await firebase
             .Child("Member")
             .OnceAsync<Member>
             ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();

            var allinter = await Select_GetAllInterItem(mem_Id);

            await firebase
                .Child("Member")
                .Child(toAddInter.Key)
                .Child("Inter_item")
                .OnceAsync<Inter_item>();

            return allinter.Where(a => a.Sell_num == s_num).FirstOrDefault();
        }
        // 관심 게시글 삭제
        public async Task Delete_InterItem(string mem_id, int s_num)
        {
            var toDelete = (await firebase
                .Child("Member")
                .OnceAsync<Member>
                ()).Where(a => a.Object.MemberID == mem_id).FirstOrDefault();
            var toDeleteInterItem = (await firebase
                .Child("Member")
                .Child(toDelete.Key)
                .Child("Inter_item")
                .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();

            await firebase.Child("Member").Child(toDelete.Key).Child("Inter_item").Child(toDeleteInterItem.Key).DeleteAsync();
        }
        //경매 종료 후 관심 게시글 삭제
        public async Task Soldout_Delete_InterItem(int s_num)
        {
            var allmembers = await Select_GetAllMembers();
            foreach(Member cntmem in  allmembers)
            {
                var noti = await Select_GetItem(s_num);
                var toDelete = (await firebase
                .Child("Member")
                .OnceAsync<Member>
                ()).Where(a => a.Object.MemberID == cntmem.MemberID).FirstOrDefault();
                var toDeleteInterItem = (await firebase
                    .Child("Member")
                    .Child(toDelete.Key)
                    .Child("Inter_item")
                    .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();

                if(toDeleteInterItem != null)
                {
                    await AddNoti(cntmem.MemberID, noti.Sell_name, noti.Sell_num, noti.Sell_ID, noti.Topbid_ID, noti.Highest_bid, 3);
                    await firebase.Child("Member").Child(toDelete.Key).Child("Inter_item").Child(toDeleteInterItem.Key).DeleteAsync();
                }
               
            }
        }
        // 게시글 추가
        public async Task AddItem(string s_name, string s_con, string s_img, int s_price, int h_bid, int s_mode
            , int s_cate, string s_id)
        {
            int s_num = 0;
            var allItem = await Select_GetAllItem();
            while(true)
            {
                if (allItem.Where(a => a.Sell_num == s_num).FirstOrDefault() != null)
                {
                    s_num++;
                }
                else
                {
                    break;
                }           
            }
            await firebase
              .Child("SellingItem")
              .PostAsync(new SellingItem()
              {
                  Sell_category = s_cate,
                  Sell_contents = s_con,
                  Sell_ID = s_id,
                  Sell_img = s_img,
                  Sell_mode = s_mode,
                  Sell_name = s_name,
                  Sell_num = s_num,
                  Sell_price = s_price,
                  Highest_bid = h_bid,
                  Topbid_ID = "",
                  Views_cnt = 0

              });
        }
        //게시글 전체 목록
        public async Task<List<SellingItem>> Select_GetAllItem()
        {
            return (await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>()).Select(Item => new SellingItem
                {
                    Sell_name = Item.Object.Sell_name,
                    Sell_category = Item.Object.Sell_category,
                    Sell_contents = Item.Object.Sell_contents,
                    Sell_ID = Item.Object.Sell_ID,
                    Sell_img = Item.Object.Sell_img,
                    Sell_mode = Item.Object.Sell_mode,
                    Sell_num = Item.Object.Sell_num,
                    Sell_price = Item.Object.Sell_price,
                    Highest_bid = Item.Object.Highest_bid,
                    Topbid_ID = Item.Object.Topbid_ID,
                    Views_cnt = Item.Object.Views_cnt
                }).ToList();
        }
        //선택된 게시글
        public async Task<SellingItem> Select_GetItem(int no)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Sell_num == no).FirstOrDefault();
        }
        //경매중인 상품 게시글 검색
        public async Task<List<SellingItem>> Select_GetAllsellingItems(string sell_ID)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Sell_ID == sell_ID).ToList();
        }
        //경매 중인 판매자의 게시글검색 
        public async Task<List<SellingItem>> Select_GetSeller(string seller_ID)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Sell_ID == seller_ID).ToList();
        }
        //경매 중 최고 입찰자인 게시글검색 
        public async Task<List<SellingItem>> Select_GetAlltopbidItems(string top_ID)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Topbid_ID == top_ID).ToList();
        }
        // 카테고리 게시글 검색
        public async Task<List<SellingItem>> Select_GetCategory(int cate_num)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Sell_category == cate_num).ToList();
        }
        // 게시글 이름 검색
        public async Task<List<SellingItem>> Select_GetName(string item_name)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
            return allItem.Where(a => a.Sell_name .Contains(item_name)).ToList();
        }
        //top 10 게시글 검색
        public async Task<List<SellingItem>> Select_GetTop10()
        {
            var allItem = await Select_GetAllItem();
            int[] v_cnt_order10 = new int[allItem.Count];
            int i = 0;
            foreach (SellingItem s_item in allItem)
            {
                v_cnt_order10[i] = s_item.Views_cnt;
                i++;
            }
            if(v_cnt_order10.Length < 10)
            {
                for (i = 0; i < v_cnt_order10.Length; i++)
                {
                    for (int j = i + 1; j < v_cnt_order10.Length; j++)
                    {
                        if (v_cnt_order10[i] <= v_cnt_order10[j])
                        {
                            int temp = v_cnt_order10[i];
                            v_cnt_order10[i] = v_cnt_order10[j];
                            v_cnt_order10[j] = temp;
                        }
                    }
                }
                await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();
                return allItem.Where(a => a.Views_cnt >= v_cnt_order10.Length).ToList();

            }
            else
            {
                for (i = 0; i < v_cnt_order10.Length; i++)
                {
                    for (int j = i + 1; j < v_cnt_order10.Length; j++)
                    {
                        if (v_cnt_order10[i] <= v_cnt_order10[j])
                        {
                            int temp = v_cnt_order10[i];
                            v_cnt_order10[i] = v_cnt_order10[j];
                            v_cnt_order10[j] = temp;
                        }
                    }
                }
                await firebase
                    .Child("SellingItem")
                    .OnceAsync<SellingItem>();
                return allItem.Where(a => a.Views_cnt >= v_cnt_order10[9]).ToList();
            }
            
        }
        //게시글 삭제
        public async Task Delete_Item(int s_num)
        {
            
            var toDeleteItem = (await firebase
                .Child("SellingItem")
                .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();
            await firebase.Child("SellingItem").Child(toDeleteItem.Key).DeleteAsync();
            
        }
        //게시글 업데이트
        public async Task Update_Item(int s_num, string s_name, string s_con, string s_img, int s_price, int h_bid, int s_mode, int s_cate)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();

            var item = allItem.Where(a => a.Sell_num == s_num).FirstOrDefault();

            var toUpdateItem = (await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();

            await firebase
                .Child("SellingItem")
                .Child(toUpdateItem.Key)
                .PutAsync(new SellingItem()
                {
                    Sell_num = item.Sell_num,
                    Sell_name = s_name,
                    Sell_category = s_cate,
                    Sell_contents = s_con,
                    Sell_ID = item.Sell_ID,
                    Sell_img = s_img,
                    Sell_price = s_price,
                    Highest_bid = h_bid,
                    Sell_mode = s_mode,
                    Topbid_ID = "",
                    Views_cnt = 0
                });

        }
        //게시글 조회수 증가
        public async Task Update_cnt(int s_num)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();

            var item = allItem.Where(a => a.Sell_num == s_num).FirstOrDefault();

            var toUpdateItem = (await firebase
                .Child("SellingItem")
                .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();

            await firebase
                .Child("SellingItem")
                .Child(toUpdateItem.Key)
                .PutAsync(new SellingItem()
                {
                    Sell_name = item.Sell_name,
                    Sell_category = item.Sell_category,
                    Sell_contents = item.Sell_contents,
                    Sell_ID = item.Sell_ID,
                    Sell_img = item.Sell_img,
                    Sell_mode = item.Sell_mode,
                    Sell_num = item.Sell_num,
                    Sell_price = item.Sell_price,
                    Highest_bid = item.Highest_bid,
                    Topbid_ID = item.Topbid_ID,
                    Views_cnt = item.Views_cnt + 1
                });
        }
        //최고입찰가 갱신
        public async Task Update_bid(int s_num, string tbid_id, int h_bid)
        {
            var allItem = await Select_GetAllItem();
            await firebase
                .Child("SellingItem")
                .OnceAsync<SellingItem>();

            var toUpdateItem = (await firebase
                .Child("SellingItem")
                .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == s_num).FirstOrDefault();

            var item = allItem.Where(a => a.Sell_num == s_num).FirstOrDefault();

            await firebase
                .Child("SellingItem")
                .Child(toUpdateItem.Key)
                .PutAsync(new SellingItem()
                {
                    Sell_name = item.Sell_name,
                    Sell_category = item.Sell_category,
                    Sell_contents = item.Sell_contents,
                    Sell_ID = item.Sell_ID,
                    Sell_img = item.Sell_img,
                    Sell_mode = item.Sell_mode,
                    Sell_num = item.Sell_num,
                    Sell_price = item.Sell_price,
                    Topbid_ID = tbid_id,
                    Highest_bid = h_bid,
                    Views_cnt = item.Views_cnt
                });
        }
        //구매완료 추가
        public async Task AddbuyItem(string mem_Id, string s_name, string s_con, string s_img, int s_price, string s_id , string t_id, int h_bid, int s_mode)
        {
            int s_num = 0;
            var allItem = await Select_GetAllBuyItem(mem_Id);
            while (true)
            {
                if (allItem.Where(a => a.Sell_num == s_num).FirstOrDefault() != null)
                {
                    s_num++;
                }
                else
                {
                    break;
                }
            }
            var toAddbuyItem = (await firebase
              .Child("Member")
              .OnceAsync<Member>
              ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            await firebase
              .Child("Member")
              .Child(toAddbuyItem.Key)
              .Child("BuyComplete")
              .PostAsync(new BuyComplete()
              {
                  Sell_contents = s_con,
                  Sell_name = s_name,
                  Sell_ID = s_id,
                  Sell_img = s_img,
                  Sell_num = s_num,
                  Sell_price = s_price,
                  Topbid_ID = t_id,
                  Highest_bid = h_bid,
                  Sell_mode = s_mode

              });
        }
        //구매완료 전체 검색
        public async Task<List<BuyComplete>> Select_GetAllBuyItem(string mem_Id)
        {
            var toAddbuyItem = (await firebase
              .Child("Member")
              .OnceAsync<Member>
              ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            return (await firebase
                .Child("Member")
                .Child(toAddbuyItem.Key)
                .Child("BuyComplete")
                .OnceAsync<BuyComplete>()).Select(Item => new BuyComplete
                {
                    Sell_name = Item.Object.Sell_name,
                    Sell_contents = Item.Object.Sell_contents,
                    Sell_ID = Item.Object.Sell_ID,
                    Sell_img = Item.Object.Sell_img,
                    Sell_num = Item.Object.Sell_num,
                    Sell_price = Item.Object.Sell_price,
                    Topbid_ID = Item.Object.Topbid_ID,
                    Highest_bid = Item.Object.Highest_bid,
                    Sell_mode = Item.Object.Sell_mode
                }).ToList();
        }
        //구매완료 검색
        public async Task<BuyComplete> Select_GetBuyItem(string mem_Id, int no)
        {
            var toAddbuyItem = (await firebase
              .Child("Member")
              .OnceAsync<Member>
              ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();

            var allbuyitems = await Select_GetAllBuyItem(mem_Id);

            await firebase
                .Child("Member")
                .Child(toAddbuyItem.Key)
                .Child("BuyComplete")
                .OnceAsync<BuyComplete>();

            return allbuyitems.Where(a => a.Sell_num == no).FirstOrDefault();
        }
        //판매완료 추가
        public async Task AddsoldoutItem(string mem_Id, string s_name, string s_con, string s_img, int s_price, string s_id, string t_id, int h_bid, int s_mode)
        {
            int s_num = 0;
            var allItem = await Select_GetAllSoldOutItem(mem_Id);
            while (true)
            {
                if (allItem.Where(a => a.Sell_num == s_num).FirstOrDefault() != null)
                {
                    s_num++;
                }
                else
                {
                    break;
                }
            }
            var toAddsoldoutItem = (await firebase
              .Child("Member")
              .OnceAsync<Member>
              ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            await firebase
                .Child("Member")
                .Child(toAddsoldoutItem.Key)
                .Child("SoldOutItem")
                .PostAsync(new SoldOutItem()
              {
                  Sell_contents = s_con,
                  Sell_name = s_name,
                  Sell_ID = s_id,
                  Sell_img = s_img,
                  Sell_num = s_num,
                  Sell_price = s_price,
                  Topbid_ID = t_id,
                  Highest_bid = h_bid,
                  Sell_mode = s_mode

                });
        }
        //판매완료 전체 검색
        public async Task<List<SoldOutItem>> Select_GetAllSoldOutItem(string mem_Id)
        {
            var toAddsoldoutItem = (await firebase
             .Child("Member")
             .OnceAsync<Member>
             ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            return (await firebase
                .Child("Member")
                .Child(toAddsoldoutItem.Key)
                .Child("SoldOutItem")
                .OnceAsync<SoldOutItem>()).Select(Item => new SoldOutItem
                {
                    Sell_name = Item.Object.Sell_name,
                    Sell_contents = Item.Object.Sell_contents,
                    Sell_ID = Item.Object.Sell_ID,
                    Sell_img = Item.Object.Sell_img,
                    Sell_num = Item.Object.Sell_num,
                    Sell_price = Item.Object.Sell_price,
                    Topbid_ID = Item.Object.Topbid_ID,
                    Sell_mode = Item.Object.Sell_mode,
                    Highest_bid = Item.Object.Highest_bid
                }).ToList();
        }
        //판매완료 검색
        public async Task<SoldOutItem> Select_GetSoldOutItem(string mem_Id, int no)
        {
            var toAddsoldoutItem = (await firebase
              .Child("Member")
              .OnceAsync<Member>
              ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();

            var allbuyitems = await Select_GetAllSoldOutItem(mem_Id);

            await firebase
                .Child("Member")
                .Child(toAddsoldoutItem.Key)
                .Child("SoldOutItem")
                .OnceAsync<SoldOutItem>();

            return allbuyitems.Where(a => a.Sell_num == no).FirstOrDefault();
        }

        //----------------------------------------------------------------------------------------------------------------

        //--------------------------------------------알림관련-------------------------------------------------------

        // 알림 추가
        public async Task AddNoti(string mem_Id, string s_name, int sitem_num ,string s_id, string t_id, int h_bid, int p_sw)
        {
            int s_num = 0;
            string noti_con;
            var allnoti =  await Select_GetAllINoti(mem_Id);
            var allmembers = await Select_GetAllMembers();
            while (true)
            {
                if (allnoti.Where(a => a.noti_num == s_num).FirstOrDefault() != null)
                {
                    s_num++;
                }
                else
                {
                    break;
                }
            }
            var toAddnoti = (await firebase
                .Child("Member")
                .OnceAsync<Member>
                ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();



            //페이지 선택 스위치 1 : 즐겨찾기 및 경매 중,  2 : 구매완료 , 3 : 판매완료
            if (p_sw == 1)
            {
                noti_con = s_id + "님이 판매중인 " + s_name + "제목의 제품의 가격이" + h_bid + " 로 변동되었습니다.";
                await firebase
                        .Child("Member")
                        .Child(toAddnoti.Key)
                        .Child("Noti")
                        .PostAsync(new Noti()
                        {
                            noti_num = s_num,
                            noti = noti_con,
                            sellitem_num = sitem_num,
                            page_sw = p_sw
                        });
                foreach (Member cntmem in allmembers)
                {
                    var tocheck = (await firebase
                    .Child("Member")
                    .OnceAsync<Member>
                    ()).Where(a => a.Object.MemberID == cntmem.MemberID).FirstOrDefault();
                    var tocheckInterItem = (await firebase
                        .Child("Member")
                        .Child(tocheck.Key)
                        .Child("Inter_item")
                        .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == sitem_num).FirstOrDefault();

                    if (tocheckInterItem != null)
                    {
                        await firebase
                        .Child("Member")
                        .Child(tocheck.Key)
                        .Child("Noti")
                        .PostAsync(new Noti()
                        {
                            noti_num = s_num,
                            noti = noti_con,
                            sellitem_num = sitem_num,
                            page_sw = p_sw,
                            memid = t_id
                        });
                    }

                }
            }
            else if(p_sw == 2)
            {
                noti_con = s_id + "님이 판매중인 " + s_name + "제목의 제품이 낙찰되었습니다.";
                await firebase
                        .Child("Member")
                        .Child(toAddnoti.Key)
                        .Child("Noti")
                        .PostAsync(new Noti()
                        {
                            noti_num = s_num,
                            noti = noti_con,
                            sellitem_num = sitem_num,
                            page_sw = p_sw,
                            memid = s_id
                        }) ;
                foreach (Member cntmem in allmembers)
                {
                    var tocheck = (await firebase
                    .Child("Member")
                    .OnceAsync<Member>
                    ()).Where(a => a.Object.MemberID == cntmem.MemberID).FirstOrDefault();
                    var tocheckInterItem = (await firebase
                        .Child("Member")
                        .Child(tocheck.Key)
                        .Child("Inter_item")
                        .OnceAsync<Inter_item>()).Where(a => a.Object.Sell_num == sitem_num).FirstOrDefault();

                    if (tocheckInterItem != null)
                    {
                        await firebase
                        .Child("Member")
                        .Child(tocheck.Key)
                        .Child("Noti")
                        .PostAsync(new Noti()
                        {
                            noti_num = s_num,
                            noti = noti_con,
                            sellitem_num = sitem_num,
                            page_sw = p_sw,
                            memid = t_id
                        });
                    }

                }
            }
            else
            {
                noti_con = "현재 판매중인 " + s_name + "제목의 제품이 "+ t_id +"님 에게 구매되었습니다.";
                await firebase
                        .Child("Member")
                        .Child(toAddnoti.Key)
                        .Child("Noti")
                        .PostAsync(new Noti()
                        {
                            noti_num = s_num,
                            noti = noti_con,
                            sellitem_num = sitem_num,
                            page_sw = p_sw
                        });
            }
            

        }
        //관심 게시글 검색
        public async Task<List<Noti>> Select_GetAllINoti(string mem_Id)
        {
            var toAddnoti = (await firebase
             .Child("Member")
             .OnceAsync<Member>
             ()).Where(a => a.Object.MemberID == mem_Id).FirstOrDefault();
            return (await firebase
                .Child("Member")
                .Child(toAddnoti.Key)
                .Child("Noti")
                .OnceAsync<Noti>()).Select(Item => new Noti
                {
                    noti = Item.Object.noti,
                    noti_num = Item.Object.noti_num,
                    sellitem_num = Item.Object.sellitem_num
                    
                }).ToList();
        }
        // 관심 게시글 삭제
        public async Task Delete_Noti(string mem_id, int s_num)
        {
            var tonoti = (await firebase
                .Child("Member")
                .OnceAsync<Member>
                ()).Where(a => a.Object.MemberID == mem_id).FirstOrDefault();
            var toDeletenoti = (await firebase
                .Child("Member")
                .Child(tonoti.Key)
                .Child("Noti")
                .OnceAsync<Noti>()).Where(a => a.Object.noti_num == s_num).FirstOrDefault();

            await firebase.Child("Member").Child(tonoti.Key).Child("Noti").Child(toDeletenoti.Key).DeleteAsync();
        }

    }
}
