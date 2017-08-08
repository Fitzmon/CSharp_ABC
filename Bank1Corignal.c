#include <stdio.h>
#include <string.h>
#include <stdlib.h>

/* ����� */
#define INTERESTRATE 0.002 //���� 
/* ����һ����� */
char** pName; //����
char** pID; //���֤��
//int* index; //˳���
char** account; //�˺�
double* balance; //���
int* startYear; //��ʼ��
int* startMonth; //��ʼ��
int counts; //�����˻�����

/* �ַ����ȽϺ��� */
int _strcmp(char* str1, char* str2) {
	int i = 0;
	int temp = 0; //??
	int ans = 0; //??
	//���ַ��Ƚ�
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

/* �����˻����� */
char* CreateAccount(char aName[],char aID[]) {
	int n = strlen(aName)+strlen(aID); //���������ַ�����ĳ���
	char* strcount = new char[n];
	strcpy(strcount,strcat(aName,aID)); //�������ַ����鿽����
	//�µ��ַ�������Ϊ�˺���
	return strcount;
}

/* ɾ���˻����� */
int DeleteAccount(char dID[]) {
	int i = 0;
	//������ɾ�����û�ID
	for(; i<counts; i++) {
		if(_strcmp(dID,pID[i])==0) {
			break;
		}
	}
	//����ҵ����˻���ɾ�����˻�����λ����Ϣ
	if(i<counts) {
		for(int j =1; j<counts-1; j++) { //??������7������?j+1ֵ�ĸ���?
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

/* ��Ǯ���� */
void Save(char whichAccount[],double inMoney) {
	for(int i =0; i<counts; i++) {
		if(_strcmp(whichAccount,account[i])==0) { //�����˻�
			break;
		}
		if(i<counts)
			balance[i]+=inMoney;
	}
}

/* ������Ϣ���������¼��� */
double SetInterest(int index,int Year,int Month) {
	return balance[index]*INTERESTRATE*((Year-startYear[index])*12\
	                                    +Month-startMonth[index]);
}

/* ȡǮ���� */
bool Withdraw(char whichAccount[],double outMoney) {
	for(int i =0; i<counts; i++) { //�����˻�
		if(_strcmp(whichAccount,account[i])==0)
			break;
	}
	if(i<counts) {
		double interest = SetInterest(i,2011,5);
		if(balance[i]+interset<outMoney)
			return false; //���ȡǮ��Ŀ�����˻�������\
		���¼��㵱ǰ���
		balance[i]=outMoney+interest;
		//ע���˴��������¸��´�Ǯ����
	}
	return true;
}

/* ������ģ�� */
int main(int argc,char*argv[]) {
	counts= 0;
	//����m�����˻�
	printf("How many accounts do you like to build?\n");
	static int m = 0; // mΪ��̬���������溯���ĵ��ú��˳���������
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
	//����ÿһ���˻�������ID����Ϣ
	while(scanf("%s%s\n",name,id)>0&&i<m) { //scanf����ֵ��ʾ�ɹ���ȡ��������
		pName[i] = new char[100];
		pID[i] = new char[16];
		strcpy(pName[i],name);
		strcpy(pID[i],id);
		i++;
	}
	//ģ���˻�����
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
	//����ڴ�
	delete[]pName;
	delete[]pID;
	delete[]account;
	delete[]balance;
	delete[]startYear; 
	delete[]startMonth;
	return 0; 
}
