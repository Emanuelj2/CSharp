#include <iostream>

class Foo
{
private:
	int m_x{};
public:

	Foo(int x)
		: m_x{ x }
	{

	}

	int getValueX()const
	{
		return m_x;
	}
};

void printFoo(Foo f)
{
	std::cout << f.getValueX() << std::endl;
}

int main()
{
	printFoo(5);

	return 0;
}