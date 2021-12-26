using System;
using System.Collections.Generic;
using System.Text;

namespace Final_usedAuction.Models
{
    class Member
    {
            public string MemberID { get; set; } // 아이디
            public string MemberPW { get; set; } // 비밀번호
            public string MemberName { get; set; } // 이름
            public string MemberNName { get; set; } // 계정명
            public string MemberEMail { get; set; } // 이메일
            public string MemberKAID { get; set; } // 카톡아이디
            public string MemberPhone { get; set; } // 전화번호
            public int MemberT_count { get; set; } // 거래횟수
            public int MemberS_count { get; set; } // 판매횟수
            public int MemberB_count { get; set; } // 구매횟수
            public int MemberTrust { get; set; } // 신뢰도 점수
            public int MemberAnswer { get; set; } // 응답률 점수
            public int MemberSatissfied { get; set; } // 만족도 점수
    }
}
