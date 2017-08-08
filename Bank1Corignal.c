#include <stdio.h>
#include <string.h>
#include <stdlib.h>

/* 定义宏 */
#define INTERESTRATE 0.002 //利率 
/* 定义一组变量 */
char** pName; //姓名
char** pID; //身份证号
//int* index; //顺序号
char** account; //账号
double* balance; //余额
int* startYear; //开始年
int* startMonth; //开始月
int counts; //银行账户总数

/* 字符串比较函数 */
int _strcmp(char* str1, char* str2) {
	int i = 0;
	int temp = 0; //??
	int ans = 0; //??
	//逐字符比较
	while((str1[i]!='\0')&&(str2[i]!='\0')) {
		temp = str1[i]-str2[i];
		if(temp==0) {
			i++;
			continue;
		} else {
			ans =temp;
			break;
		}
	}
	return ans;
}

/* 创建账户函数 */
char* CreateAccount(char aName[],char aID[]) {
	int n = strlen(aName)+strlen(aID); //计算两个字符数组的长度
	char* strcount = new char[n];
	strcpy(strcount,strcat(aName,aID)); //将两个字符数组拷贝到
	//新的字符数组作为账号名
	return strcount;
}

/* 删除账户函数 */
int DeleteAccount(char dID[]) {
	int i = 0;
	//查找需删除的用户ID
	for(; i<counts; i++) {
		if(_strcmp(dID,pID[i])==0) {
			break;
		}
	}
	//如果找到该账户，删除该账户所在位置信息
	if(i<counts) {
		for(int j =1; j<counts-1; j++) { //??创建了7个数组?j+1值的覆盖?
			pName[j] = pName[j+1];
			pID[j] = pID[j+1];
			account[j] = account[j+1];
			balance[j] = balance[j+1];
			startYear[j] = startYear[j+1];
			startMonth[j] = startMonth[j+1];
			balance[j] = balance[j+1];
		}
		return counts-1;
	}
	return counts;
}

/* 存钱函数 */
void Save(char whichAccount[],double inMoney) {
	for(int i =0; i<counts; i++) {
		if(_strcmp(whichAccount,account[i])==0) { //查找账户
			break;
		}
		if(i<counts)
			balance[i]+=inMoney;
	}
}

/* 计算利息函数，按月计算 */
double SetInterest(int index,int Year,int Month) {
	return balance[index]*INTERESTRATE*((Year-startYear[index])*12\
	                                    +Month-startMonth[index]);
}

/* 取钱函数 */
bool Withdraw(char whichAccount[],double outMoney) {
	for(int i =0; i<counts; i++) { //查找账户
		if(_strcmp(whichAccount,account[i])==0)
			break;
	}
	if(i<counts) {
		double interest = SetInterest(i,2011,5);
		if(balance[i]+interset<outMoney)
			return false; //如果取钱数目大于账户余额，出错\
		重新计算当前余额
		balance[i]=outMoney+interest;
		//注：此处还需重新更新存钱日期
	}
	return true;
}

/* 主程序模拟 */
int main(int argc,char*argv[]) {
	counts= 0;
	//创建m个空账户
	printf("How many accounts do you like to build?\n");
	static int m = 0; // m为静态变量，不随函数的调用和退出而发生变
	scanf("%d\n",&m);
	if(m<1) return 0;

	pName = new char*[m];
	pID = new char*[m];
	account = new char*[m];
	balance = new double[m];
	startYear = new int[m];
	startMonth = new int[m];

	printf("Please input the guest's name and ID.\n");
	char name[100];
	char id[16];
	int i = 0;
	//输入每一个账户姓名和ID的信息
	while(scanf("%s%s\n",name,id)>0&&i<m) { //scanf返回值表示成功读取变量个数
		pName[i] = new char[100];
		pID[i] = new char[16];
		strcpy(pName[i],name);
		strcpy(pID[i],id);
		i++;
	}
	//模拟账户操作
	bool bExit = false;
	do {
		printf("Please select one business.1 stand for Adding a account;\
		2 stand for Deleting a account;3 stand for Saving;4 stand for \
		Withdrawing;0 stand for Exit\n");
		int s = 0;
		scanf("%d\n",&s);
		switch(s) {
			case 1:
				if(counts<m) {
					printf("Please input your name and ID\n");
					char name[100];
					char id[16];
					scanf("%s%s\n",name,id);
					pName[counts]=name;
					pID[counts]=id;
					startYear[counts]=2013;
					startMonth[counts]=4;
					balance[counts]=0.0;
					account[counts++]=CreatAccount(name,id);
				} else {
					printf("Sorry, no any empty account for you\n");
				}
				break;
			case 2:
				if(counts>0) {
					printf("Please input your ID\n");
					char id[16];
					scanf("%s\n",id);
					id[15]='\0';
					counts = DeleteAccount(id);
				} else {
					printf("No any account for deleting\n");
				}
				break;
			case 3:
				printf("What's your Account?\n");
				char cs[116];
				scanf("%s\n",cs);
				sc[strlen(cs)]='\0';
				printf("How much would you save?\n");
				double rs;
				scanf("%f\n",&rs);
				Save(cs,rs);
				break;
			case 4:
				printf("What's your Account?\n");
				char cd[116];
				scanf("%s\n",cd);
				cd[strlen(cd)] = '\0';
				printf("How much would you withdraw?\n");
				double rd;
				scanf("%f\n",&rd);
				Withdraw(cd,rd);
				break;
			default:
				bExit=true;
				break;

		}
	}while(!bExit)
	//清除内存
	delete[]pName;
	delete[]pID;
	delete[]account;
	delete[]balance;
	delete[]startYear; 
	delete[]startMonth;
	return 0; 
}
